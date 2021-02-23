using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using Unity.Reflect.Utils;

namespace Unity.Reflect.Services
{
    /// <summary>
    /// This struct specifies information about a specific project that has been discovered by the <see cref="ProjectDiscovery"/>.
    /// </summary>
    public struct ProjectInfo
    {
        public string ServerProjectId { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string ServerName { get; set; }
        public string ServerAddress { get; set; }
        public string UserId { get; set; }
        public string OrgId { get; set; }
        public DateTime LastUpdated { get; set; }
        public TargetChannel ServiceChannel { get; set; }
    }

    /// <summary>
    /// This class allows services to be discovered over the local network.
    /// </summary>
    public class ProjectDiscovery
    {
        ServiceDiscovery m_ServiceDiscovery;

        /// <summary>
        /// This event is triggered when a project is added to the available project list.
        /// </summary>
        public event Action<ProjectInfo> OnProjectAdded;

        /// <summary>
        /// This event is triggered when a project is removed from the available project list.
        /// </summary>
        public event Action<ProjectInfo> OnProjectRemoved;

        /// <summary>
        /// A list of all available projects that have been discovered by the ProjectDiscovery.
        /// </summary>
        public static IReadOnlyDictionary<string, ProjectInfo> availableProjects
        {
            get
            {
                lock (s_AvailableProjects)
                {
                    return s_AvailableProjects.ToDictionary(e => e.Key, e => e.Value);
                }
            }
        }

        static Dictionary<string, ProjectInfo> s_AvailableProjects = new Dictionary<string, ProjectInfo>();

        /// <summary>
        /// Creates a ProjectDiscovery
        /// </summary>
        /// <param name="isServer">Whether the user of the Discovery is a server</param>
        public ProjectDiscovery(bool isServer)
        {
            m_ServiceDiscovery = new ServiceDiscovery(isServer);
            m_ServiceDiscovery.onServiceChanged += ServiceDiscovery_ServiceChangedNotify;
        }

        void ServiceDiscovery_ServiceChangedNotify(ServiceChanged e)
        {
            if(e.Status.Equals(AvailableServiceStatus.Connected))
            {
                var serviceAddress = $"{e.Service.Address}:{e.Service.Port}";
                Logger.Info($"Found Service '{e.Service.Name}' running at {serviceAddress}");
                try
                {
                    var client = new Client.ClientBase(e.Service.ServiceChannel);
                    client.Connect();
                    var serverProjectList = client.ListUnityProject();
                    if(serverProjectList.Any())
                    {
                        Logger.Debug($"Found {serverProjectList.Length} Projects on {e.Service.Name}");
                        AddProjects(e.Service.ServiceChannel, serverProjectList);
                    }
                    client.Disconnect(false);
                }
                catch (Exception ex)
                {
                    Logger.Info($"Could not get service projects: '{ex.Message}'");
                    m_ServiceDiscovery.RemoveAvailableService(e.Service.Name);
                }
            }

            if(e.Status.Equals(AvailableServiceStatus.Disconnected))
            {
                var unavailableProjects = s_AvailableProjects.Where(x => x.Value.ServerName.Equals(e.Service.Name)).Select(x => x.Key).ToList();
                Logger.Info($"Found {unavailableProjects.Count} unavailable projects from removed service {e.Service.Name}");
                if (unavailableProjects.Count > 0)
                {
                    lock(s_AvailableProjects)
                    {
                        foreach (var projectKey in unavailableProjects)
                        {
                            if (s_AvailableProjects.ContainsKey(projectKey))
                            {
                                OnProjectRemoved?.Invoke(s_AvailableProjects[projectKey]);
                                s_AvailableProjects.Remove(projectKey);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts the discovery.
        /// </summary>
        public void Start()
        {
            lock (s_AvailableProjects)
            {
                s_AvailableProjects.Clear();
            }

            m_ServiceDiscovery?.Start();
        }

        /// <summary>
        /// Stops the discovery.
        /// </summary>
        public void Stop()
        {
            lock (s_AvailableProjects)
            {
                s_AvailableProjects.Clear();
            }

            m_ServiceDiscovery?.Stop();
            m_ServiceDiscovery?.Destroy();
        }

        void AddProjects(Channel channel, IEnumerable<Reflect.UnityProject> projects)
        {
            lock (s_AvailableProjects)
            {
                foreach (var project in projects)
                {
                    if (!s_AvailableProjects.ContainsKey(project.Id))
                    {
                        var availableProject = new ProjectInfo
                        {
                            ServerProjectId = project.Id,
                            ProjectId = project.ProjectId,
                            Name = project.Name,
                            ServerName = project.ServerName,
                            ServerAddress = project.ServerAddress,
                            UserId = "",
                            OrgId = "",
                            LastUpdated = DateTime.Now,
                            ServiceChannel = new TargetChannel(channel)
                        };
                        s_AvailableProjects.Add(project.Id, availableProject);
                        OnProjectAdded?.Invoke(availableProject);
                    }
                }
            }
        }
    }
}
