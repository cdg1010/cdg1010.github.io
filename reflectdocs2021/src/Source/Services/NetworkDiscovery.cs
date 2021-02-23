using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Unity.Reflect.Utils;

namespace Unity.Reflect.Services
{
    class ServiceDiscoveryEventArgs : EventArgs
    {
        public string Address { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
    }

    class LocalNetworkIPChangedEventArgs : EventArgs
    {
        public string Address { get; set; }
    }

    struct IPv4EndPoint 
    {
        public IPEndPoint EndPoint;
        public string Address;
        public string BroadcastAddress;
        public string NetworkName;
    }

    // UDP broadcasting
    class NetworkDiscovery
    {
        public event EventHandler<ServiceDiscoveryEventArgs> ServiceDiscoveryNotify;
        public event EventHandler<LocalNetworkIPChangedEventArgs> LocalNetworkIPChangedNotify;

        public bool isLocalNetworkConnected;

        Dictionary<string, IPInterfaceProperties> m_NetworkProperties;

        Socket m_ServiceDiscoverySocket;
        EndPoint m_ServiceDiscoveryEndPoint;
        UdpClient m_UdpDiscoveryClient;
        
        readonly bool k_IsServer;
        // Server & player listen on these ports for discovery
        readonly int k_DiscoveryStartPort = 13370;
        readonly int k_DiscoveryPortIncrement = 300;
        readonly int k_DiscoveryMaxPort = 43370;
        
        // Average discovery message is usually less then 100 bytes 
        readonly int k_PacketSize = 1024;
        readonly string k_ServerHandShake = "UnitySyncServer";
        readonly string k_ClientHandShake = "UnitySyncClient";
        const int k_LocalNetworkRange = 254;
        const string k_IPAddressFilePath = "C://reflect/ip.txt";
        
        int m_CurrentDiscoveryPort;
        public static int serviceInternalPort = 0;
        public static int serviceExternalPort = 0;
        public static string serviceExternalAddress = "";
        public string ipv4Address = "";

        Dictionary<string, IPv4EndPoint> m_IPv4EndPoints = new Dictionary<string, IPv4EndPoint>();

        HashSet<string> m_PermissionDeniedIPv4Addresses = new HashSet<string>();
        HashSet<string> m_NoHostRouteIPv4Addresses = new HashSet<string>();

        bool ShuttingDown = false;

        public NetworkDiscovery(bool isServer)
        {
            k_IsServer = isServer;
            m_IPv4EndPoints.Clear();
        }

        IEnumerable<int> DiscoveryPorts()
        {
            var discoveryPort = k_DiscoveryStartPort;

            while (discoveryPort < k_DiscoveryMaxPort)
            {
                yield return discoveryPort;
                discoveryPort += k_DiscoveryPortIncrement;
            }
        }
        
        public void CreateDiscoveryEndPoints()
        {
            CreateDiscoveryClient();
            CreateDiscoverySocket();
        }

        void CreateDiscoverySocket()
        {
            try
            {
                m_ServiceDiscoverySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                m_ServiceDiscoverySocket.ExclusiveAddressUse = false;
                m_ServiceDiscoverySocket.EnableBroadcast = true;
            }
            catch (Exception) { }

            if (m_ServiceDiscoverySocket == null)
            {
                Logger.Error("m_ServiceDiscoverySocket creation failed");
            }
            else
            {
                InitDiscoveryEndPoint();
            }
        }

        void CreateDiscoveryClient()
        {
            m_UdpDiscoveryClient = new UdpClient(AddressFamily.InterNetwork);
            m_UdpDiscoveryClient.EnableBroadcast = true;
        }
        
        void InitDiscoveryEndPoint()
        {
            foreach (var discoveryPort in DiscoveryPorts())
            {
                if (m_ServiceDiscoveryEndPoint == null)
                {
                    try
                    {
                        m_ServiceDiscoveryEndPoint = new IPEndPoint(IPAddress.Any, discoveryPort);
                        m_ServiceDiscoverySocket.Bind(m_ServiceDiscoveryEndPoint);
                        m_CurrentDiscoveryPort = discoveryPort;
                        Logger.Info($"listening to m_CurrentDiscoveryPort:{m_CurrentDiscoveryPort}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"Could not bind Discovery Endpoint at port {discoveryPort}. {ex.Message}");
                        m_ServiceDiscoveryEndPoint = null;
                    }
                }
            }
            
            if( m_ServiceDiscoveryEndPoint != null)
            {
                SocketClientWaitForNextIncomingMessage();
            }
            else
            {
                Logger.Warn($"Could not create Discovery Endpoint");
            }
        }

        // Finds an available port and tests it can be bound to. Returns port in `port`.
        public void GetAnyAvailablePort(IPAddress ipAddress, out int port)
        {
            var anyPortTcpListener = new TcpListener(ipAddress, 0);
            anyPortTcpListener.Start();
            port = ((IPEndPoint)anyPortTcpListener.LocalEndpoint).Port;
            anyPortTcpListener.Stop();
        }

        // Tests a given port can be bound to
        public void TestPortBinding(IPAddress ipAddress, int port)
        {
            var anyPortTcpListener = new TcpListener(ipAddress, port);
            anyPortTcpListener.Start();
            anyPortTcpListener.Stop();
        }

        public string GetBroadcastAddress(string address)
        {
            return $"{address.Remove(address.LastIndexOf(".", StringComparison.InvariantCulture))}.255";
        }
 
        public void CloseClient()
        {
            Logger.Info("CloseClient");
            ShuttingDown = true;
            CloseDiscoverySocket();
            CloseDiscoveryClient();
        }

        void CloseDiscoverySocket()
        {
            if (m_ServiceDiscoverySocket != null)
            {
                m_ServiceDiscoverySocket.Close();
                m_ServiceDiscoverySocket = null;
            }
            m_ServiceDiscoveryEndPoint = null;
        }

        void CloseDiscoveryClient()
        {
            if(m_UdpDiscoveryClient != null)
            {
                m_UdpDiscoveryClient.Close();
                m_UdpDiscoveryClient = null;
            }
        }

        void ResetDiscoveryClient()
        {
            if(!ShuttingDown)
            {
                CloseDiscoveryClient();
                CreateDiscoveryClient();
                m_PermissionDeniedIPv4Addresses.Clear();
                m_NoHostRouteIPv4Addresses.Clear();
            }
        }
 
        void SocketClientWaitForNextIncomingMessage()
        {
            m_ServiceDiscoverySocket.BeginReceiveFrom(new byte[k_PacketSize], 0, k_PacketSize, SocketFlags.None, ref m_ServiceDiscoveryEndPoint, OnSocketClientReceive, m_ServiceDiscoveryEndPoint);
        }

        protected virtual void OnServiceDiscovery(ServiceDiscoveryEventArgs e)
        {
            ServiceDiscoveryNotify?.Invoke(this, e);
        }

        protected virtual void OnLocalNetworkIPChanged(LocalNetworkIPChangedEventArgs e)
        {
            LocalNetworkIPChangedNotify?.Invoke(this, e);
        }

        void OnSocketClientReceive(IAsyncResult asyncResult)
        {   
            if (m_ServiceDiscoverySocket != null)
            {
                try
                {
                    ProcessSingleEndPointMessage(asyncResult);
                    SocketClientWaitForNextIncomingMessage();
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Exception occured on socket client data receive:{ex}");
                    if(!ShuttingDown)
                    {
                        Logger.Info($"Try recovering from disconnected socket");
                        CloseDiscoverySocket();
                        CreateDiscoverySocket();
                    }
                }
            }
        }

        void ProcessSingleEndPointMessage(IAsyncResult asyncResult) 
        {         
            EndPoint endPoint = asyncResult.AsyncState as EndPoint;
            byte[] byte_buffer = new byte[k_PacketSize];
            int num = m_ServiceDiscoverySocket.ReceiveFrom(byte_buffer, k_PacketSize, SocketFlags.None, ref endPoint);
            string message = null;
            if (num < k_PacketSize)
            {
                byte[] array = new byte[num];
                Buffer.BlockCopy(byte_buffer, 0, array, 0, num);
                message = Encoding.ASCII.GetString(array);
                //Logger.Info($"Got {num} bytes from '{endPoint}', '{message}'.");
            }
            m_ServiceDiscoverySocket.EndReceiveFrom(asyncResult, ref endPoint);

            if(IsAnyDiscoveryMessage(message))
            {
                var endPointAddress = endPoint.ToString();
                //Logger.Debug($"endPointAddress:{endPointAddress}:message:{message}");
                var addressNoPort = endPointAddress.Split(':')[0];
                var addressPort = endPointAddress.Split(':')[1];

                if (m_IPv4EndPoints.ContainsKey(addressNoPort))
                {
                    OnLocalNetworkIPDiscovered(addressNoPort);
                }

                if (k_IsServer)
                {
                    OnClientDiscovered(message);
                    if(!message.Equals(GetLocation()))
                    {
                        OnServiceDiscovered(message);
                    }
                }
                else
                {
                    OnServiceDiscovered(message);
                }
            }
        }
        void OnLocalNetworkIPDiscovered(string address)
        {
            if (!isLocalNetworkConnected)
            {
                Logger.Info($"Local Network IP discovered: '{address}'.");
                isLocalNetworkConnected = true;
                if (string.IsNullOrEmpty(ipv4Address))
                {
                    ipv4Address = address;
                }
                OnLocalNetworkIPChanged(new LocalNetworkIPChangedEventArgs { Address = address });
            }
            else
            {
                // Second time
                if (!ipv4Address.Equals(address))
                {
                    // We only allow client to update their ipv4Address
                    if (k_IsServer)
                    {
                        // TODO: Restart server and activate grpc services on new ipv4Address.
                    }
                    else
                    {
                        var newDiscoveredIP = TryGetExternallyVisibleIPFromSocket();
                        if (!string.IsNullOrEmpty(newDiscoveredIP))
                        {
                            if (!ipv4Address.Equals(newDiscoveredIP))
                            {
                                Logger.Warn($"Client changed network. Now using IP {newDiscoveredIP}");
                                ipv4Address = newDiscoveredIP;
                                OnLocalNetworkIPChanged(new LocalNetworkIPChangedEventArgs { Address = newDiscoveredIP });
                            }
                        }
                    }
                }
            }
        }

        void OnServiceDiscovered(string serviceAddress)
        {
            if(serviceAddress.StartsWith($"{k_ServerHandShake}://") && serviceAddress.Split('/').Length == 4)
            {
                var splitAddress = serviceAddress.Split('/');
                var incomingAddress = splitAddress[2];
                var incomingServiceName = WebUtility.UrlDecode(splitAddress[3]);
                OnServiceDiscovery(new ServiceDiscoveryEventArgs { Address = incomingAddress.Split(':')[0], Port = incomingAddress.Split(':')[1], Name = incomingServiceName });
            }
        }

        void OnClientDiscovered(string clientAddress)
        {
            if(clientAddress.StartsWith($"{k_ClientHandShake}://") && clientAddress.Split('/').Length == 3)
            {
                var splitAddress = clientAddress.Split('/');
                var incomingAddress = splitAddress[2];
                var addressNoPort = incomingAddress.Split(':')[0];
                var addressPort = incomingAddress.Split(':')[1];

                SendServiceLocation(addressNoPort, addressPort);
            }
        }
        
        bool IsAnyDiscoveryMessage(string message)
        {
            return !string.IsNullOrEmpty(message) && (message.StartsWith(k_ServerHandShake) || message.StartsWith(k_ClientHandShake));
        }

        public static string GetServiceAddress()
        {
            if(string.IsNullOrEmpty(serviceExternalAddress))
            {
                 return $"127.0.0.1:{serviceInternalPort}";
            }
            else
            {
                return $"{serviceExternalAddress}:{serviceExternalPort}";
            }
        }

        public static string GetLocalMachineName()
        {
            try
            {
                return $"{Environment.MachineName}-{Environment.UserName}";
            }
            catch (Exception) { }
            return Environment.UserName;
        }

        static string TryGetExternallyVisibleIPFromSocket()
        {
            string localIP = null;
            // If not manually set in a text file, use socket connect to find the externally visible ip address.
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    var endPoint = (IPEndPoint) socket.LocalEndPoint;
                    localIP = endPoint.Address.ToString();
                }
                catch (Exception)
                {
                    Logger.Warn($"No externally visible network available from socket.");
                }
            }
            return localIP;
        }

        bool TryGetExternallyVisibleIP(out string localIP)
        {
            localIP = null;
            if(PlatformUtils.k_IsWindows && System.IO.File.Exists(k_IPAddressFilePath))
            {
                localIP = System.IO.File.ReadAllText(k_IPAddressFilePath).Trim(' ');
                Logger.Debug($"using config file IP:'{localIP}'");
            }
            if(string.IsNullOrEmpty(localIP))
            {
                localIP = TryGetExternallyVisibleIPFromSocket();
            }
            return !string.IsNullOrEmpty(localIP);
        }

        public void BroadcastSelfDiscoverySignal()
        {
            if(m_ServiceDiscoveryEndPoint != null && !isLocalNetworkConnected)
            {
                // Get all broadcast available network
                RefreshLocalBroadcastEndPoints();
                var message = Encoding.ASCII.GetBytes(k_IsServer ? k_ServerHandShake : k_ClientHandShake);
                if(TryGetExternallyVisibleIP(out string externalVisibleIP))
                {
                    // Try to find ourself with our specific IP LAN address
                    // This will confirm that the visible ip is reachable
                    var endPoint = new IPEndPoint(IPAddress.Parse(externalVisibleIP), m_CurrentDiscoveryPort);
                    if(!SendEndPointMessage(externalVisibleIP, endPoint, message))
                    {
                        ResetDiscoveryClient();
                    }
                }
                else
                {
                    // Try to find our ip using broadcast available network
                    foreach (KeyValuePair<string, IPv4EndPoint> ipv4EndPoint in m_IPv4EndPoints)
                    {
                        if(!SendEndPointMessage(ipv4EndPoint.Value.BroadcastAddress, ipv4EndPoint.Value.EndPoint, message))
                        {
                            ResetDiscoveryClient();
                        }
                    }
                }
            }
        }

        public void BroadcastLocation()
        {
            if(isLocalNetworkConnected)
            {
                var messageString = GetLocation();
                //Logger.Info($"BroadcastLocation:'{messageString}'");
                var message = Encoding.ASCII.GetBytes(messageString);

                var broadcastMessageSent = false;
                foreach (KeyValuePair<string, IPv4EndPoint> ipv4EndPoint in m_IPv4EndPoints)
                {
                    var endPointAddress = ipv4EndPoint.Value.EndPoint.Address.ToString();
                    if(!m_PermissionDeniedIPv4Addresses.Contains(endPointAddress) && !m_NoHostRouteIPv4Addresses.Contains(endPointAddress))
                    {
                        foreach (var discoveryPort in DiscoveryPorts())
                        {
                            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(endPointAddress), discoveryPort);
                            if(SendEndPointMessage(endPointAddress, endPoint, message))
                            {
                                broadcastMessageSent = true;
                            }
                            else
                            {
                                // Try to recover from SocketError
                                Logger.Warn($"Try recovering from SocketError.");
                                ResetDiscoveryClient();
                            }
                        }
                    }
                }

                // If broadcast is disabled or inaccessible, fallback to full LAN range ping strategy.
                if(!broadcastMessageSent)
                {     
                    var subAddress = ipv4Address.Remove(ipv4Address.LastIndexOf(".", StringComparison.InvariantCulture));
                    //SendReportLogEvent($"Cannot Broadcast. Send full LAN range on '{subAddress}.*'");
                    for (int j = 1; j<= k_LocalNetworkRange; j++ )
                    {
                        var lanIPAddress = $"{subAddress}.{j}";
                        if(lanIPAddress.Equals(ipv4Address) || m_PermissionDeniedIPv4Addresses.Contains(lanIPAddress) || m_NoHostRouteIPv4Addresses.Contains(lanIPAddress))
                        {
                            continue;
                        }

                        foreach (var discoveryPort in DiscoveryPorts())
                        {
                            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(lanIPAddress), discoveryPort);
                            if(!SendEndPointMessage(lanIPAddress, endPoint, message))
                            {
                                // Try to recover from SocketError
                                Logger.Warn($"Try recovering from SocketError.");
                                ResetDiscoveryClient();
                            }
                        }
                    }
                }
            }
        }

        public void SendServiceLocation(string address, string port)
        {
            if(isLocalNetworkConnected)
            {
                var clientEndPoint = new IPEndPoint(IPAddress.Parse(address), int.Parse(port));
                var messageString = GetLocation();
                //Logger.Info($"SendServiceLocation: '{messageString}' to {address}:{port}");
                var message = Encoding.ASCII.GetBytes(messageString);
                SendEndPointMessage(address, clientEndPoint, message);
            }
        }

        string GetLocation()
        {
            if(k_IsServer)
            {
                return $"{k_ServerHandShake}://{ipv4Address}:{serviceExternalPort}/{WebUtility.UrlEncode(GetLocalMachineName())}";
            }
            else
            {
                return $"{k_ClientHandShake}://{ipv4Address}:{m_CurrentDiscoveryPort}";
            }
        }

        bool SendEndPointMessage(string address, IPEndPoint endPoint, byte[] message) 
        {
            try
            {
                m_UdpDiscoveryClient.Send(message, message.Length, endPoint);
            }
            catch (SocketException ex)
            {
                // Socket was removed or disconnected somehow (iOS device is put to Sleep or sent to BackgroundTask)
                if(ex.SocketErrorCode == SocketError.Shutdown || ex.SocketErrorCode == SocketError.NotConnected)
                {
                    Logger.Warn($"SocketError Shutdown or NotConnected. Should Recover DiscoveringEndPoints");
                    return false;
                }

                Logger.Info($"SocketError on '{address}': ErrorCode '{ex.ErrorCode}': {ex.Message}");
                if (ex.SocketErrorCode == SocketError.AccessDenied)
                {
                    m_PermissionDeniedIPv4Addresses.Add(address);
                }
                if (ex.SocketErrorCode == SocketError.NetworkUnreachable)
                {
                    m_NoHostRouteIPv4Addresses.Add(address);
                }
                if (ex.SocketErrorCode == SocketError.HostDown)
                {
                    m_NoHostRouteIPv4Addresses.Add(address);
                }
                if (ex.SocketErrorCode == SocketError.HostUnreachable)
                {
                    m_NoHostRouteIPv4Addresses.Add(address);
                }
            }
            return true;
        }

        void RefreshLocalBroadcastEndPoints()
        {
            if (m_NetworkProperties == null) 
            {
                // This call hangs on OSX the second time its called.
                m_NetworkProperties = new Dictionary<string, IPInterfaceProperties>();
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var network in networkInterfaces)
                {
                    m_NetworkProperties.Add(network.Name, network.GetIPProperties());
                }  
            }
            
            foreach (var networkProperty in m_NetworkProperties)
            {
                foreach (var address in networkProperty.Value.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    var validIPv4Address = address.Address.ToString();
                    if (validIPv4Address.LastIndexOf(".", StringComparison.InvariantCulture) != -1)
                    {
                        var broadcastAddress = GetBroadcastAddress(validIPv4Address);
                        if(!m_IPv4EndPoints.ContainsKey(validIPv4Address) && !m_PermissionDeniedIPv4Addresses.Contains(broadcastAddress)) 
                        {
                            var newIPv4EndPoint = new IPv4EndPoint
                            {
                                EndPoint = new IPEndPoint(IPAddress.Parse(broadcastAddress), m_CurrentDiscoveryPort),
                                Address = validIPv4Address,
                                BroadcastAddress = broadcastAddress,
                                NetworkName = networkProperty.Key
                            };
                            m_IPv4EndPoints.Add(validIPv4Address, newIPv4EndPoint);
                        }
                    }
                }
            }
            var defaultBroadcastAddress = IPAddress.Broadcast.ToString();
            if (!m_IPv4EndPoints.ContainsKey(defaultBroadcastAddress))
            {
                var newIPv4EndPoint = new IPv4EndPoint
                {
                    EndPoint = new IPEndPoint(IPAddress.Parse(defaultBroadcastAddress), m_CurrentDiscoveryPort),
                    Address = defaultBroadcastAddress,
                    BroadcastAddress = defaultBroadcastAddress,
                    NetworkName = ""
                };
                m_IPv4EndPoints.Add(defaultBroadcastAddress, newIPv4EndPoint);
            }
        }
    }
}