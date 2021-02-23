using System;
using System.Runtime.CompilerServices;
using Unity.Reflect.Services.Client;

[assembly: InternalsVisibleTo("UI")] // Temporary while common UI not available
[assembly: InternalsVisibleTo("SyncService")] // Temporary until we split projects
[assembly: InternalsVisibleTo("ReflectTests")] // Temporary until we split projects
namespace Unity.Reflect
{
    /// <summary>
    ///     An <see cref="IPlayerClient"/> factory.
    /// </summary>
    public static class Player
    {
        /// <summary>
        ///     Creates and connects an <see cref="IPlayerClient"/> to a SyncServer.
        /// </summary>
        /// <param name="channelAddress">The IP address and the port of the SyncServer, following the format "XXX.XXX.XXX.XXX:YYYYY". If null, the client will try to connect to a local SyncServer.</param>
        /// <returns>A player client that is connected to the SyncServer.</returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        public static IPlayerClient CreateClient(string channelAddress = null)
        {
            return CreateClient(new TargetChannel(ClientManager.CreateChannel(channelAddress)));
        }

        /// <summary>
        ///     Creates and connects an <see cref="IPlayerClient"/> to a SyncServer.
        /// </summary>
        /// <param name="channel">The target channel establishing a connection to the SyncServer. Cannot be null.</param>
        /// <returns>A player client that is connected to the SyncServer.</returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        /// <exception cref="ArgumentException">If the channel is null</exception>
        public static IPlayerClient CreateClient(TargetChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException("channel cannot be null.");

            var player = new PlayerClient(channel.Channel);
            player.Connect();
            return player;
        }
    }

    /// <summary>
    ///     An <see cref="IPublisherClient"/> factory.
    /// </summary>
    public static class Publisher
    {
        /// <summary>
        ///     Creates and connects a blocking <see cref="IPublisherClient"/> to a SyncServer.
        /// </summary>
        /// <param name="sourceProjectName">The name of the source project</param>
        /// <param name="sourceProjectId">A persistent source project ID</param>
        /// <param name="userId">The ID of the Unity user publishing their project</param>
        /// <param name="destinationProjectId">The ID of the target Unity Project that is to host the data</param>
        /// <param name="settings">Some custom publishing settings</param>
        /// <param name="channelAddress">The address of the SyncServer, following the format "XXX.XXX.XXX.XXX:YYYYY". If null, the client will try to connect to a local SyncServer.</param>
        /// <returns>A publisher client that is connected to the SyncServer, whose transaction operations are blocking.</returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        public static IPublisherClient OpenClient(string sourceName, string sourceId, string userId, string destinationProjectId, PublisherSettings settings, string channelAddress = null)
        {
            return OpenClient(new TargetChannel(ClientManager.CreateChannel(channelAddress)), sourceName, sourceId, userId, destinationProjectId, settings);
        }

        /// <summary>
        ///     Creates and connects a blocking <see cref="IPublisherClient"/> to a SyncServer.
        /// </summary>
        /// <param name="channel">The target channel establishing a connection to the SyncServer. Cannot be null.</param>
        /// <param name="sourceProjectName">The name of the source project</param>
        /// <param name="sourceProjectId">A persistent source project ID</param>
        /// <param name="userId">The ID of the Unity user publishing their project</param>
        /// <param name="destinationProjectId">The ID of the target Unity Project that is to host the data</param>
        /// <param name="settings">Some custom publishing settings</param>
        /// <returns>A publisher client that is connected to the SyncServer, whose transaction operations are blocking.</returns>
        /// <exception cref="ConnectionException">If the connection with
        the SyncServer fails</exception>
        /// <exception cref="ArgumentException">If the channel is null</exception>
        public static IPublisherClient OpenClient(TargetChannel channel, string sourceName, string sourceId, string userId, string destinationProjectId, PublisherSettings settings)
        {
            if (channel == null)
                throw new ArgumentNullException("channel cannot be null.");

            var publisher = new PublisherClientSync(channel.Channel);
            publisher.Connect();
            publisher.CreateSession(sourceName, sourceId, userId, destinationProjectId, settings);
            return publisher;
        }

        /// <summary>
        ///     Creates and connects an asynchronous <see cref="IPublisherClient"/> to a SyncServer (experimental).
        /// </summary>
        /// <param name="sourceProjectName">The name of the source project</param>
        /// <param name="sourceProjectId">A persistent source project ID</param>
        /// <param name="userId">The ID of the Unity user publishing their project</param>
        /// <param name="destinationProjectId">The ID of the target Unity Project that is to host the data</param>
        /// <param name="settings">Some custom publishing settings</param>
        /// <param name="nbWorkers">The number of worker threads allowed in the thread pool</param>
        /// <param name="channelAddress">The address of the SyncServer, following the format "XXX.XXX.XXX.XXX:YYYYY". If null, the client will try to connect to a local SyncServer.</param>
        /// <returns>A publisher client that is connected to the SyncServer, whose transaction operations are asynchronous.</returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        public static IPublisherClient OpenAsyncClient(string sourceName, string sourceId, string userId, string destinationProjectId, PublisherSettings settings, int nbWorkers = 1, string channelAddress = null)
        {
            return OpenAsyncClient(new TargetChannel(ClientManager.CreateChannel(channelAddress)), sourceName, sourceId, userId, destinationProjectId, settings);
        }

        /// <summary>
        ///     Creates and connects an asynchronous <see cref="IPublisherClient"/> to a SyncServer (experimental).
        /// </summary>
        /// <param name="channel">The target channel establishing a connection to the SyncServer. Cannot be null.</param>
        /// <param name="sourceProjectName">The name of the source project</param>
        /// <param name="sourceProjectId">A persistent source project ID</param>
        /// <param name="userId">The ID of the Unity user publishing their project</param>
        /// <param name="destinationProjectId">The ID of the target Unity Project that is to host the data</param>
        /// <param name="settings">Some custom publishing settings</param>
        /// <param name="nbWorkers">The number of worker threads allowed in the thread pool</param>
        /// <returns>A publisher client that is connected to the SyncServer, whose transaction operations are asynchronous.</returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        /// <exception cref="ArgumentException">If the channel is null</exception>
        public static IPublisherClient OpenAsyncClient(TargetChannel channel, string sourceName, string sourceId, string userId, string destinationProjectId, PublisherSettings settings, int nbWorkers = 1)
        {
            if (channel == null)
                throw new ArgumentNullException("channel cannot be null.");

            var publisher = new PublisherClientAsync(channel.Channel, nbWorkers);
            publisher.Connect();
            publisher.CreateSession(sourceName, sourceId, userId, destinationProjectId, settings);
            return publisher;
        }
    }

    /// <summary>
    /// An <see cref="IClient"/> factory.
    /// </summary>
    /// <seealso cref="Publisher"/>
    /// <seealso cref="Player"/>
    public static class BaseClient
    {
        /// <summary>
        ///     Creates and connects an <see cref="IClient"/> to a SyncServer.
        /// </summary>
        /// <param name="autoconnect">Whether the client automatically tries to connect. If false, you can connect it yourself, by using its Connect method.</param>
        /// <param name="channelAddress">The address of the SyncServer, following the format "XXX.XXX.XXX.XXX:YYYYY". If null, the client will try to connect to a local SyncServer.</param>
        /// <returns>A client that is connected to the SyncServer. </returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        public static IClient CreateClient(bool autoconnect = true, string channelAddress = null)
        {
            return CreateClient(new TargetChannel(ClientManager.CreateChannel(channelAddress)), autoconnect);
        }

        /// <summary>
        ///     Creates and connects an <see cref="IClient"/> to a SyncServer.
        /// </summary>
        /// <param name="channel">The target channel establishing a connection to the SyncServer. Cannot be null.</param>
        /// <param name="autoconnect">Whether the client automatically tries to connect. If false, you can connect it yourself by using its Connect method.</param>
        /// <returns>A client that is connected to the SyncServer. </returns>
        /// <exception cref="ConnectionException">If the connection with the SyncServer fails</exception>
        /// <exception cref="ArgumentException">If the channel is null</exception>
        public static IClient CreateClient(TargetChannel channel, bool autoconnect = true)
        {
            if (channel == null)
                throw new ArgumentNullException("channel cannot be null.");

            var client = new ClientBase(channel.Channel);

            if (autoconnect)
                client.Connect();

            return client;
        }
    }

    /// <summary>
    ///     Stores information about an established connection with a SyncServer.
    /// </summary>
    public class TargetChannel
    {
        internal Grpc.Core.Channel Channel { get; set; }

        /// <summary>
        ///     Shows the address and the port of the designed SyncServer, in the format "XXX.XXX.XXX.XXX:YYYYY".
        /// </summary>
        public string Target
        {
            get
            {
                return Channel.Target;
            }
        }

        internal TargetChannel(Grpc.Core.Channel channel)
        {
            Channel = channel;
        }
    }
}
