using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Grpc.Core;
using Unity.Reflect.Utils;
using Unity.Reflect.Constants;

namespace Unity.Reflect.Services
{
    struct ServiceChanged
    {
        public AvailableServiceInfo Service { get; set; }
        public AvailableServiceStatus Status { get; set; }
    }

    enum AvailableServiceStatus
    {
        Connected = 0,
        Disconnected = 1
    }
    
    struct AvailableServiceInfo
    {
        public string Address { get; set; }
        public string Port { get; set; }
        public Channel ServiceChannel { get; set; }
        public string Name { get; set; }
    }

    class ServiceDiscovery
    {
        public event Action<ServiceChanged> onServiceChanged;
        
        NetworkDiscovery m_NetworkDiscovery;
        Timer m_NetworkDiscoveryTimer;

        int m_CurrentLocalNetworkDiscoveryTryCount = 0;
        readonly int k_MaxLocalNetworkDiscoveryTryCount = 3;

        const int k_MaxReceivingMessageLength = 100;
        const int k_MaxSendingMessageLength = 100;
        const int k_MaxConcurrentStreams = 32;
        readonly bool k_IsServer;

        Dictionary<string, AvailableServiceInfo> m_AvailableServices = new Dictionary<string, AvailableServiceInfo>();
        HashSet<string> m_TryConnectingServers = new HashSet<string>();

        public ServiceDiscovery(bool isServer = true, int refreshInterval = 5000, int maxLocalNetworkDiscoveryTryCount = 3)
        {
            if (SyncConstants.k_IsCloudClient)
            {
                // TODO cloud-hack
                var args = new ServiceDiscoveryEventArgs{
                    Address = SyncConstants.k_PlayerServiceAddress,
                    Port    = $"{SyncConstants.k_ServicePort}",
                    Name    = SyncConstants.k_PlayerServiceName,
                };
                Task.Run(() => AddServiceOnConnectAsync(args));
            }
            else
            {
                k_IsServer = isServer;
                k_MaxLocalNetworkDiscoveryTryCount = maxLocalNetworkDiscoveryTryCount;
                m_NetworkDiscovery = new NetworkDiscovery(k_IsServer);
                
                m_NetworkDiscoveryTimer = new Timer(refreshInterval);
            }
        }

        public void Start()
        {
            if (SyncConstants.k_IsCloudClient) {
                return;
            }

            if(!m_NetworkDiscoveryTimer.Enabled)
            {
                m_NetworkDiscovery.ServiceDiscoveryNotify += NetworkDiscovery_ServiceDiscoveryNotify;
                m_NetworkDiscoveryTimer.Elapsed += OnRefreshNetworkDiscovery;
                m_NetworkDiscoveryTimer.Start();
                if(!m_NetworkDiscovery.isLocalNetworkConnected)
                {
                    m_NetworkDiscovery.CreateDiscoveryEndPoints();
                    m_NetworkDiscovery.BroadcastSelfDiscoverySignal();
                }
            }
        }

        public void Stop()
        {
            if (SyncConstants.k_IsCloudClient) {
                return;
            }

            if(m_NetworkDiscoveryTimer.Enabled)
            {
                m_NetworkDiscoveryTimer.Stop();
                m_NetworkDiscoveryTimer.Elapsed -= OnRefreshNetworkDiscovery;
                m_NetworkDiscovery.ServiceDiscoveryNotify -= NetworkDiscovery_ServiceDiscoveryNotify;
            }
        }

        public void Destroy()
        {
            Logger.Info("ServiceDiscovery.Destroy");
            lock(m_AvailableServices)
            {
                foreach(var service in m_AvailableServices.Values)
                {
                    if(!service.ServiceChannel.State.Equals(ChannelState.Shutdown))
                    {
                        Task.Run(async () => await service.ServiceChannel.ShutdownAsync());
                    }
                }
            }
            m_NetworkDiscovery?.CloseClient();
        }
        
        protected virtual void OnServiceChangedEvent(ServiceChanged e)
        {
            onServiceChanged?.Invoke(e);
        }
        
        void OnRefreshNetworkDiscovery(object source, ElapsedEventArgs e)
        {
            if(!m_NetworkDiscovery.isLocalNetworkConnected) 
            {
                if(m_CurrentLocalNetworkDiscoveryTryCount < k_MaxLocalNetworkDiscoveryTryCount)
                {
                    m_NetworkDiscovery.BroadcastSelfDiscoverySignal();
                    m_CurrentLocalNetworkDiscoveryTryCount++;
                }
                else
                {
                    Logger.Debug("No Local Network Available.");
                    Stop();
                }
            }
            else
            {
                if(!k_IsServer)
                {
                    m_NetworkDiscovery.BroadcastLocation();
                }
            }
        }

        static async Task<Channel> TryChannelConnection(string address)
        {
            var channelOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.MaxConcurrentStreams, k_MaxConcurrentStreams),
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength, k_MaxReceivingMessageLength * 1024 * 1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, k_MaxSendingMessageLength * 1024 * 1024)
            };

            // Wrap with try/catch as if the runtime lib is not found, this can fail silently
            Channel channel = null;
            try {
                channel = new Channel(address, ChannelCredentials.Insecure, channelOptions);
            } catch (System.DllNotFoundException e) {
                Logger.Error($"FATAL: gRPC runtime not found (check com.unity.reflect/Plugins/Grpc.Core/runtimes/... for the library file). Exception follows: {e}");
            }
            return await TryChannelConnection(channel);
        }

        static async Task<Channel> TryChannelConnection(Channel channel)
        {
            var deadline = DateTime.UtcNow.AddSeconds(3);
            try
            {
                await channel.ConnectAsync(deadline);
                if(channel.State == ChannelState.Ready)
                {
                    return channel;
                }
            }
            catch (TaskCanceledException)
            {
                await channel.ShutdownAsync();
            }
            return null;
        }

        async Task WatchForChannelDisconnection(string name, Channel channel)
        {
            try
            {
                while(true)
                {
                    if(channel.State.Equals(ChannelState.Shutdown))
                    {
                        break;
                    }
                    if(!await channel.TryWaitForStateChangedAsync(channel.State))
                    {
                        break;
                    }
                    // State changed, we will try reconnect before awaiting next state change
                    if(await TryChannelConnection(channel) == null)
                    {
                        break;
                    }
                }
            }
            catch (TaskCanceledException)
            {
                await channel.ShutdownAsync();
            }
            Logger.Info($"channel was shutdown:{name}:{channel.State}");
            lock(m_AvailableServices)
            {
                if(m_AvailableServices.ContainsKey(name))
                {
                    OnServiceChangedEvent(new ServiceChanged()
                    {
                        Service = m_AvailableServices[name],
                        Status = AvailableServiceStatus.Disconnected
                    });
                    m_AvailableServices.Remove(name);
                }
                if(m_TryConnectingServers.Contains(name))
                {
                    m_TryConnectingServers.Remove(name);
                }
            }
        }
        
        async void AddServiceOnConnectAsync(ServiceDiscoveryEventArgs e)
        {
            if(!m_TryConnectingServers.Contains(e.Name))
            {
                m_TryConnectingServers.Add(e.Name);
                var connectedChannel = await TryChannelConnection($"{e.Address}:{e.Port}");
                if(connectedChannel != null)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(() => WatchForChannelDisconnection(e.Name, connectedChannel));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    lock(m_AvailableServices)
                    {
                        if(!m_AvailableServices.ContainsKey(e.Name))
                        {
                            m_AvailableServices.Add(e.Name, new AvailableServiceInfo { Address = e.Address, Port = e.Port, ServiceChannel = connectedChannel, Name = e.Name });
                            OnServiceChangedEvent(new ServiceChanged()
                            {
                                Service = m_AvailableServices[e.Name],
                                Status = AvailableServiceStatus.Connected,
                            });
                        }
                    }
                }
                m_TryConnectingServers.Remove(e.Name);
            }
        }
        
        public void RemoveAvailableService(string name)
        {
            lock(m_AvailableServices)
            {
                if(m_AvailableServices.ContainsKey(name))
                {
                    m_AvailableServices.Remove(name);
                }
            }
        }

        void NetworkDiscovery_ServiceDiscoveryNotify(object sender, ServiceDiscoveryEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Name))
            {
                if(!m_AvailableServices.ContainsKey(e.Name))
                {
                    Task.Run(() => AddServiceOnConnectAsync(e));
                }
                else
                {
                    // If server changed address
                    if(!m_AvailableServices[e.Name].Address.Equals(e.Address))
                    {
                       OnServiceChangedEvent(new ServiceChanged()
                        {
                            Service = m_AvailableServices[e.Name],
                            Status = AvailableServiceStatus.Disconnected,
                        });
                        m_AvailableServices.Remove(e.Name);
                        Task.Run(() => AddServiceOnConnectAsync(e));
                    }
                }
            }
        }
    }
}