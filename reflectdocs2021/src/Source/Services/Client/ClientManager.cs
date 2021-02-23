using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Unity.Reflect.Utils;
using System.Xml;
using System.Runtime.Serialization;

namespace Unity.Reflect.Services.Client
{
    enum ServiceStatus
    {
        Connected = 0,
        Disconnected = 1
    }

    class HealthState
    {
        public bool isHealthy;
        public bool hasError;
        public string errorMessage = "";
    }

    struct ObserveEventArgs
    {
        public readonly string ObservableId;
        public readonly Observable ObservableType;

        public ObserveEventArgs(string observableId, Observable observableType)
        {
            ObservableId = observableId;
            ObservableType = observableType;
        }
    }

    struct StreamEventArgs
    {
        public readonly string Id;
        public readonly CancellationTokenSource ObserveCancellationTokenSource; // TODO hide
        public readonly ConnectionStatus Status;

        public StreamEventArgs(string id, CancellationTokenSource observeCancellationTokenSource, ConnectionStatus status)
        {
            Id = id;
            ObserveCancellationTokenSource = observeCancellationTokenSource;
            Status = status;
        }
    }
    
    class ClientManager
    {
        public event Action<ObserveEventArgs> ObserveEventNotify;
        public event Action<StreamEventArgs> StreamEventNotify;
        CancellationTokenSource m_ObserveCancellationTokenSource;
        IAsyncStreamReader<ObserveNotification> m_AsyncStreamReader;

        readonly string m_ServiceConfigInfoPath;
        
        static readonly int k_MaxReceivingMessageLength = 100;
        static readonly int k_MaxSendingMessageLength = 100;
        static readonly int k_MaxConcurrentStreams = 32;
        
        ServiceInfo m_ServiceInfo;
        HealthState m_State;
        Channel m_Channel;
        string m_Address;

        public SyncAgent.SyncAgentClient Client { get; private set; }
        public ServiceStatus Status { get; private set; } = ServiceStatus.Disconnected;

        public ClientManager(Channel channel)
        {
            m_Channel = channel;
            if(m_Channel == null)
            {
                // Once started, Service will write its configuration in this file.
                m_ServiceConfigInfoPath = PlatformUtils.GetLocalSyncServiceConfigPath();
            }
        }

        // Returns gRPC Channel
        public static Channel CreateChannel(string channelAddress = null)
        {
            if (channelAddress == null)
                return null;

            var channelOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.MaxConcurrentStreams, k_MaxConcurrentStreams),
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength, k_MaxReceivingMessageLength * 1024 * 1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, k_MaxSendingMessageLength * 1024 * 1024)
            };
            return new Channel(channelAddress, ChannelCredentials.Insecure, channelOptions);
        }

        bool SetupClientChannels()
        {
            if(m_Channel != null && string.IsNullOrEmpty(m_ServiceConfigInfoPath))
            {
                Client = CreateClient(m_Channel);
                return true;
            }
            else
            {
                if(ReadLocalServiceConfig())
                {
                    Logger.Info($"Service running at port:{m_ServiceInfo.internalPort}");
                    m_Address = $"127.0.0.1:{m_ServiceInfo.internalPort}";
                    m_Channel = CreateChannel(m_Address);
                    Client = CreateClient(m_Channel);
                    return true;
                }
            }
            return false;
        }
        
        public void Connect()
        {
            Logger.Info("Connecting to Syncing Service...");
            if (SetupClientChannels())
            {
                ChannelHealthCheck();
                if(IsChannelReady())
                {
                    Status = ServiceStatus.Connected;
                    return;
                }
            }

            throw new ConnectionException();
        }
        
        public void Disconnect(bool shutdownChannel)
        {
            if(Status == ServiceStatus.Connected)
            {
                if(shutdownChannel && !IsChannelShutdown())
                {
                    Task.Run(async () => {
                        await ChannelShutdown();
                    });
                }
                Status = ServiceStatus.Disconnected;
            }
        }

        Services.SyncAgent.SyncAgentClient CreateClient(Channel channel)
        {
            return new Services.SyncAgent.SyncAgentClient(channel);
        }
        
        bool ReadLocalServiceConfig()
        {
            if (File.Exists(m_ServiceConfigInfoPath))
            {
                var xmlBuilder = new StringBuilder();
                var fileAccessGranted = false;
                while(!fileAccessGranted)
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(m_ServiceConfigInfoPath, FileMode.Open, FileAccess.Read, FileShare.None))
                        using (var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                        {
                            var serializer = new DataContractSerializer(typeof(ServiceInfo));

                            m_ServiceInfo = (ServiceInfo)serializer.ReadObject(reader, true);
                            Logger.Info($"Found services running:{m_ServiceInfo.internalPort}");
                        }
                        fileAccessGranted = true;
                    }
                    catch (Exception)
                    {
                        Logger.Info("Error reading service info file.");
                    }
                }
                return true;
            }
            return false;
        }
        
        public bool IsChannelReady()
        {
            return m_Channel != null && (m_Channel.State == ChannelState.Idle || m_Channel.State == ChannelState.Ready) && m_State.isHealthy;
        }

        bool IsChannelShutdown()
        {
            return m_Channel != null && m_Channel.State == ChannelState.Shutdown;
        }

        bool IsChannelConnecting()
        {
            return m_Channel != null && m_Channel.State == ChannelState.Connecting;
        }

        async Task ChannelShutdown()
        {
            if(m_Channel != null && m_Channel.State != ChannelState.Shutdown)
            {
                await m_Channel.ShutdownAsync();
                m_Channel = null;
            }
        }

        void ChannelHealthCheck()
        {
            m_State = new HealthState();
            try
            {
                Client.HealthCheck(new Empty());
                m_State.isHealthy = true;
            }
            catch (RpcException ex)
            {
                Logger.Warn($"Error occured checking Health on Service. {ex.Message}");
                m_State.isHealthy = false;
                m_State.hasError = true;
                m_State.errorMessage = ex.Message;
            }
        }

        void OnObserveEvent(ObserveEventArgs e)
        {
            ObserveEventNotify?.Invoke(e);
        }

        void OnStreamEvent(StreamEventArgs e)
        {
            StreamEventNotify?.Invoke(e);
        }

        public void Observe(Observable observable, string observableId = null)
        {
            if (string.IsNullOrEmpty(observableId))
            {
                observableId = string.Empty;
            }

            if(m_ObserveCancellationTokenSource == null)
            {
                m_ObserveCancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => ObserveTask(new ObserveRequest
                    {
                        ObservableType = observable,
                        ObservableId = observableId
                    },
                    m_ObserveCancellationTokenSource.Token)
                );
            }
            else
            {
                // Otherwise just send the observerRequest directly
                Client.AddObservable(new ObserveRequest
                    {
                        ObservableType = observable,
                        ObservableId = observableId
                    }
                );
            }
        }

        public void Release(Observable observable, string observableId = null)
        {
            if(m_ObserveCancellationTokenSource != null && !m_ObserveCancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    if (string.IsNullOrEmpty(observableId))
                    {
                        observableId = string.Empty;
                    }
                    var releaseStatus = Client.Release(new ReleaseRequest { ObservableType = observable, ObservableId = observableId });
                    Logger.Info($"Successfully released:'{observable}'");
                    if(releaseStatus.ObserveComplete)
                    {
                        Logger.Info("Observe Completed");
                        m_ObserveCancellationTokenSource.Cancel();
                        m_ObserveCancellationTokenSource.Dispose();
                        m_ObserveCancellationTokenSource = null;
                    }
                }
                catch(Exception ex)
                {
                    Logger.Warn($"Error occured releasing sourceId:{ex.Message}");
                }
            }
        }

        async Task ObserveTask(ObserveRequest observeRequest, CancellationToken streamCancellationToken)
        {
            var clientId = Guid.NewGuid().ToString();
            
            using (var asyncServerStreamCall = Client.Observe(observeRequest, null, null, streamCancellationToken))
            {
                m_AsyncStreamReader = asyncServerStreamCall.ResponseStream;

                OnStreamEvent(new StreamEventArgs(clientId, m_ObserveCancellationTokenSource, ConnectionStatus.Connected));

                // Usage of a CancellationToken will throw an expected StatusCode of Cancelled
                try
                {
                    while (await m_AsyncStreamReader.MoveNext(streamCancellationToken))
                    {
                        OnObserveEvent(new ObserveEventArgs(m_AsyncStreamReader.Current.ObservableId, m_AsyncStreamReader.Current.ObservableType));
                    }
                }
                catch(RpcException ex)
                {
                    Logger.Info($"Catch expected exception on cancellation:{ex.Message}:{ex.StatusCode}");
                }
            }
            m_AsyncStreamReader = null;

            OnStreamEvent(new StreamEventArgs(clientId, m_ObserveCancellationTokenSource, ConnectionStatus.Disconnected));
        }
    }
}
