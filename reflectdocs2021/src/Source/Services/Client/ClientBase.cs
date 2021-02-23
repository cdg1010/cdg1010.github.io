using System;
using System.Linq;
using Grpc.Core;

namespace Unity.Reflect.Services.Client
{
    class ClientBase : IClient
    {
        public event Action<ConnectionStatus, string> OnConnectionStatusChanged;

        public event Action OnUserUpdate;
        public event Action OnUnityProjectListUpdate;
        public event Action<string> OnSessionBegin;
        public event Action<string> OnSessionEnd;
        public event Action<string> OnManifestUpdate;

        protected readonly ClientManager m_Client;

        public ClientBase(Channel channel)
        {
            m_Client = new ClientManager(channel);

            m_Client.ObserveEventNotify += OnObserveEvent;
            m_Client.StreamEventNotify += OnStream;
        }

        public bool Connected
        {
            get
            {
                return m_Client.Status == ServiceStatus.Connected;
            }
        }

        public void Connect()
        {
            m_Client.Connect();
        }

        public void Disconnect(bool shutdownChannel = true)
        {
            m_Client.Disconnect(shutdownChannel);
        }

        // Temp method. Should be removed when the common UI is done.
        public Reflect.UnityUser ShowLoginWindow()
        {
            var unityUser = m_Client.Client.GetUnityUser(new Empty());
            if (unityUser.Success && !unityUser.User.DisplayName.Equals("anonymous"))
                return unityUser.User.ToApi();

            m_Client.Client.UserLogin(new Empty());
            return unityUser.User.ToApi();
        }

        // Temp method. Should be removed when the common UI is done.
        public string ShowProjectSelectionWindow()
        {
            m_Client.Client.CreateUnityProject(new Empty());
            return "";
        }

        // Temp method. Should be removed when the common UI is done.
        public Reflect.UnityProject[] ListUnityProject(string userId = "", string organisationId = "", bool includeCloudProjects = false, bool includeRemoteProjects = false)
        {
            var response = m_Client.Client.ListUnityProject(new UnityProjectRequest {
                UserId = userId,
                OrganisationId = organisationId,
                IncludeCloudProject = includeCloudProjects,
                IncludeRemoteProjects = includeRemoteProjects
            });
            return response.Success ? response.Projects.Select(x => x.ToApi()).ToArray() : new Reflect.UnityProject[] { };
        }

        void OnObserveEvent(ObserveEventArgs obj)
        {
            switch (obj.ObservableType)
            {
                case Observable.ManifestUpdate:
                    OnManifestUpdate?.Invoke(obj.ObservableId);
                    break;
                case Observable.SessionBegin:
                    OnSessionBegin?.Invoke(obj.ObservableId);
                    break;
                case Observable.SessionEnd:
                    OnSessionEnd?.Invoke(obj.ObservableId);
                    break;
                case Observable.UnityProjectListUpdate:
                    OnUnityProjectListUpdate?.Invoke();
                    break;
                case Observable.UserUpdate:
                    OnUserUpdate?.Invoke();
                    break;
            }
        }

        void OnStream(StreamEventArgs obj)
        {
            OnConnectionStatusChanged?.Invoke(obj.Status, obj.Id);
        }

        public void ObserveUnityProjectListUpdate()
        {
            m_Client.Observe(Observable.UnityProjectListUpdate);
        }

        public void ObserveUserUpdate()
        {
            m_Client.Observe(Observable.UserUpdate);
        }

        public void ObserveSessionBegin(params string[] sourceIds)
        {
            ObserveWithParameters(Observable.SessionBegin, sourceIds);
        }

        public void ObserveSessionEnd(params string[] sourceIds)
        {
            ObserveWithParameters(Observable.SessionEnd, sourceIds);
        }

        public void ObserveManifestUpdate(params string[] sourceIds)
        {
            ObserveWithParameters(Observable.ManifestUpdate, sourceIds);
        }

        public void ReleaseUnityProjectListUpdate()
        {
            m_Client.Release(Observable.UnityProjectListUpdate);
        }

        public void ReleaseUserUpdate()
        {
            m_Client.Release(Observable.UserUpdate);
        }

        public void ReleaseSessionBegin(params string[] sourceIds)
        {
            ReleaseWithParameters(Observable.SessionBegin, sourceIds);
        }

        public void ReleaseSessionEnd(params string[] sourceIds)
        {
            ReleaseWithParameters(Observable.SessionEnd, sourceIds);
        }

        public void ReleaseManifestUpdate(params string[] sourceIds)
        {
            ReleaseWithParameters(Observable.ManifestUpdate, sourceIds);
        }

        void ObserveWithParameters(Observable observable, string[] parameters)
        {
            if (parameters.Length == 0)
            {
                m_Client.Observe(observable);
            }
            else
            {
                foreach (var param in parameters)
                {
                    m_Client.Observe(observable, param);
                }
            }
        }

        void ReleaseWithParameters(Observable observable, string[] parameters)
        {
            if (parameters.Length == 0)
            {
                m_Client.Release(observable);
            }
            else
            {
                foreach (var param in parameters)
                {
                    m_Client.Release(observable, param);
                }
            }
        }
    }
}
