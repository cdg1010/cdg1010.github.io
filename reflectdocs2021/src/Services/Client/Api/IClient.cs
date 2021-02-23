using System;

namespace Unity.Reflect
{
    /// <summary>
    ///     This interface provides some basic client features to communicate with a SyncServer.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        ///     This event is triggered whenever the connection status changes.
        /// </summary>
        event Action<ConnectionStatus, string> OnConnectionStatusChanged;


        /// <summary>
        ///     This event is triggered whenever the client receives an UnityProjectListUpdate notification from the SyncServer.
        ///     Adding a callback to this event does not start the observation process. You need to use <see cref="ObserveUnityProjectListUpdate"/> for this.
        /// </summary>
        event Action OnUnityProjectListUpdate;

        /// <summary>
        ///     This event is triggered whenever the client receives an UserUpdate notification from the SyncServer.
        ///     Adding a callback to this event does not start the observation process. You need to use <see cref="ObserveUserUpdate"/> for this.
        /// </summary>
        event Action OnUserUpdate;

        /// <summary>
        ///     This event is triggered whenever the client receives a SessionBegin notification from the SyncServer. The string parameter is the source ID of the concerned project.
        /// </summary>
        /// <remarks>Adding a callback to this event does not start the observation process. You need to use <see cref="ObserveSessionBegin(string[])"/> for this.</remarks>
        event Action<string> OnSessionBegin;

        /// <summary>
        ///     This event is triggered whenever the client receives a SessionEnd notification from the SyncServer. The string parameter is the source ID of the concerned project.
        /// </summary>
        /// <remarks>Adding a callback to this event does not start the observation process. You need to use <see cref="ObserveSessionEnd(string[])"/> for this.</remarks>
        event Action<string> OnSessionEnd;

        /// <summary>
        ///     This event is triggered whenever the client receives a ManifestUpdate notification from the SyncServer. The string parameter is the source ID of the concerned project.
        /// </summary>
        /// <remarks>Adding a callback to this event does not start the observation process. You need to use <see cref="ObserveManifestUpdate(string[])"/> for this.</remarks>
        event Action<string> OnManifestUpdate;


        /// <summary>
        ///     Start observing the UnityProjectListUpdate notification.
        /// </summary>
        /// <seealso cref="OnUnityProjectListUpdate"/>
        void ObserveUnityProjectListUpdate();

        /// <summary>
        ///     Start observing the UserUpdate notification.
        /// </summary>
        /// <seealso cref="OnUserUpdate"/>
        void ObserveUserUpdate();

        /// <summary>
        ///     Start observing the SessionBegin notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to observe. If no ID is provided, all source projects will be observed.</param>
        /// <seealso cref="OnSessionBegin"/>
        void ObserveSessionBegin(params string[] sourceIds);

        /// <summary>
        ///     Start observing the SessionEnd notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to observe. If no ID is provided, all source projects will be observed.</param>
        /// <seealso cref="OnSessionEnd"/>
        void ObserveSessionEnd(params string[] sourceIds);

        /// <summary>
        ///     Start observing the ManifestUpdate notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to observe. If no ID is provided, all source projects will be observed.</param>
        /// <seealso cref="OnManifestUpdate"/>
        void ObserveManifestUpdate(params string[] sourceIds);


        /// <summary>
        ///     Stop observing the UnityProjectListUpdate notification.
        /// </summary>
        /// <seealso cref="OnUnityProjectListUpdate"/>
        void ReleaseUnityProjectListUpdate();

        /// <summary>
        ///     Stop observing the UserUpdate notification.
        /// </summary>
        /// <seealso cref="OnUserUpdate"/>
        void ReleaseUserUpdate();

        /// <summary>
        ///     Stop observing the SessionBegin notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to release from this notification. Use the same parameters as in <see cref="ObserveSessionBegin(string[])"/></param>
        /// <seealso cref="OnSessionBegin"/>
        void ReleaseSessionBegin(params string[] sourceIds);

        /// <summary>
        ///     Stop observing the SessionEnd notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to release from this notification. Use the same parameters as in <see cref="ObserveSessionEnd(string[])"/></param>
        /// <seealso cref="OnSessionEnd"/>
        void ReleaseSessionEnd(params string[] sourceIds);

        /// <summary>
        ///     Stop observing the ManifestUpdate notification.
        /// </summary>
        /// <param name="sourceIds">The source project IDs to release from this notification. Use the same parameters as in <see cref="ObserveManifestUpdate(string[])"/></param>
        /// <seealso cref="OnManifestUpdate"/>
        void ReleaseManifestUpdate(params string[] sourceIds);


        /// <summary>
        ///     Indicates whether the client is connected to a SyncServer.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        ///     Tries to connect to the SyncServer.
        /// </summary>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        void Connect();

        /// <summary>
        ///     Disconnects from the SyncServer.
        /// </summary>
        /// <param name="shutdownChannel"></param>
        void Disconnect(bool shutdownChannel = true);
    }

    /// <summary>
    ///     Options for the current connection status of the client.
    /// </summary>
    /// <seealso cref="IClient.OnConnectionStatusChanged"/>
    public enum ConnectionStatus
    {
        /// <summary>
        ///     The client is connected to the SyncServer.
        /// </summary>
        Connected = 0,

        /// <summary>
        ///     The client is disconnected from the SyncServer.
        /// </summary>
        Disconnected = 1,
    }

    /// <summary>
    ///     This struct specifies some information about an Unity project.
    /// </summary>
    public struct UnityProject
    {
        /// <summary>
        ///     The Reflect ID of the project.
        /// </summary>
        public readonly string Id;

        /// <summary>
        ///     The Unity ID of the project.
        /// </summary>
        public readonly string ProjectId;

        /// <summary>
        ///     The project name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        ///     The name of the host SyncServer.
        /// </summary>
        public readonly string ServerName;

        /// <summary>
        ///     The address of the host SyncServer.
        /// </summary>
        public readonly string ServerAddress;

        /// <summary>
        ///     Whether the Unity Project has a local version registered on the Hub.
        /// </summary>
        public readonly bool CloudOnly;

        public UnityProject(string projectId, string id = "", string name = "", string serverName = "", string serverAddress = "", bool cloudOnly = false)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            ServerName = serverName;
            ServerAddress = serverAddress;
            CloudOnly = cloudOnly;
        }
    }

    /// <summary>
    ///     This struct specifies information about a Unity user.
    /// </summary>
    public struct UnityUser
    {
        public readonly string AccessToken;
        public readonly string OrganizationForeignKeys;
        public readonly string PrimaryOrg;
        public readonly bool Valid;
        public readonly bool Whitelisted;

        /// <summary>
        ///     The display name of the user.
        /// </summary>
        public readonly string DisplayName;

        /// <summary>
        ///     The id of the user.
        /// </summary>
        public readonly string UserId;

        /// <summary>
        ///     The name of the user.
        /// </summary>
        public readonly string Name;

        public UnityUser(string accessToken, string displayName, string organizationForeignKeys, string primaryOrg, string userId, string name, bool valid, bool whitelisted)
        {
            AccessToken = accessToken;
            DisplayName = displayName;
            OrganizationForeignKeys = organizationForeignKeys;
            PrimaryOrg = primaryOrg;
            UserId = userId;
            Name = name;
            Valid = valid;
            Whitelisted = whitelisted;
        }
    }

    /// <summary>
    ///     This exception is thrown when the connection to a SyncServer failed.
    /// </summary>
    /// <seealso cref="IClient.Connect"/>
    /// <seealso cref="BaseClient"/>
    /// <seealso cref="Player"/>
    /// <seealso cref="Publisher"/>
    public class ConnectionException : Exception
    { }
}
