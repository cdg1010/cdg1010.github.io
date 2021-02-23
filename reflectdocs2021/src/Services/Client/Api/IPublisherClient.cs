using System;
using System.Threading.Tasks;
using Unity.Reflect.Model;

namespace Unity.Reflect
{
    /// <summary>
    ///     This interface provides some Publishing client features to communicate with a SyncServer.
    /// </summary>
    public interface IPublisherClient : IClient
    {
        /// <summary>
        ///     Starts a new transaction.
        ///     This call blocks until the SyncServer acknowledges the call.
        /// </summary>
        /// <exception cref="TransactionAlreadyPendingException">If a transaction is already pending</exception>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        void StartTransaction();        // will block until the sync server returns a Transaction ID.

        /// <summary>
        ///     Commits the pending transaction.
        ///     This call blocks until the SyncServer acknowledges the commit (which means that all the transaction operations have to be acknowledged as well).
        /// </summary>
        /// <exception cref="NoTransactionPendingException">If no transaction is pending</exception>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        void CommitTransaction();

        // <summary>
        //     Rollbacks to last transaction, discarding all changes in the current transaction.
        //     This call blocks until the SyncServer acknowledges the rollback.
        // </summary>
        // <exception cref="NoTransactionPendingException">If no transaction is pending</exception>
        // <exception cref="SessionClosedException">If the session has already been closed</exception>
        // void RollbackTransaction();

        // These methods should be used between a StartTransaction and a CommitTransaction
        // In an async implementation, they return immediately, but it's possible to subscribe to the completion with the returned task
        // In a sync implementation, they are blocking and return a dummy task when done
        // Internally, these will call RequestUpload(string hash) and Upload(hash)

        /// <summary>
        ///     Sends an <see cref="ISyncModel"/> to the SyncServer.
        ///     The server will then create or overwrite the model, depending on whether the model was already in the project or not.
        /// </summary>
        /// <param name="model">The model to send to the server</param>
        /// <returns>A <see cref="Task"/> that is resolved whenever the SyncServer acknowledges the reception of the model, regardless of whether it has been commited or not.</returns>
        /// <exception cref="NoTransactionPendingException">If no transaction is pending</exception>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        Task Send(ISyncModel model);

        /// <summary>
        ///     Removes an Object Instance.
        /// </summary>
        /// <param name="objectInstanceName">The name of the ObjectInstance to remove</param>
        /// <returns>A <see cref="Task"/> that is resolved whenever the SyncServer acknowledges the deletion, regardless of whether it has been commited or not.</returns>
        /// <exception cref="NoTransactionPendingException">If no transaction is pending</exception>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        Task RemoveObjectInstance(string objectInstanceName);

        /// <summary>
        ///     Properly closes the session.
        ///     This call blocks until the SyncServer acknowledges the call.
        /// </summary>
        /// <param name="autoDisconnectClient">If true, automatically disconnects the client when the session is closed.</param>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        void CloseAndWait(bool autoDisconnectClient = true);

        /// <summary>
        ///     Closes the session and immediately returns without waiting for the SyncServer's acknowledgement.
        /// </summary>
        /// <param name="autoDisconnectClient">If true, automatically disconnects the client.</param>
        /// <exception cref="SessionClosedException">If the session has already been closed</exception>
        void Abort(bool autoDisconnectClient = true);
    }

    /// <summary>
    ///     This struct specifies multiple settings to customize the publisher.
    /// </summary>
    /// <seealso cref="Publisher"/>
    public struct PublisherSettings
    {
        public static readonly PublisherSettings Default = new PublisherSettings(LengthUnit.Meters, AxisInversion.None);

        /// <summary>
        ///     The length unit to use when exporting data to the SyncServer.
        /// </summary>
        public readonly LengthUnit LengthUnit;

        /// <summary>
        ///     The axis inversion option to use when exporting data to the SyncServer
        /// </summary>
        public readonly AxisInversion AxisInversion;

        /// <summary>
        ///     The rules that the Rule Engine will handle
        /// </summary>
        public readonly string Rules;

        /// <summary>
        ///     Creates a PublisherSettings object.
        /// </summary>
        /// <param name="unit">The length unit to use when exporting data to the SyncServer</param>
        /// <param name="axis">The axis inversion option to use when exporting data to the SyncServer</param>
        /// <param name="rules">The rules that the Rule Engine will handle</param>
        public PublisherSettings(LengthUnit unit, AxisInversion axis, string rules="")
        {
            LengthUnit = unit;
            AxisInversion = axis;
            Rules = rules;
        }
    }

    /// <summary>
    ///     Sets the length unit to use when exporting data to the SyncServer.
    /// </summary>
    /// <seealso cref="PublisherSettings"/>
    public enum LengthUnit
    {
        Meters,
        Feet,
        Inches
    }

    /// <summary>
    ///     Sets options for inverting axes when exporting data to the SyncServer.
    /// </summary>
    /// <seealso cref="PublisherSettings"/>
    public enum AxisInversion {
        /// <summary>
        ///     No axis inversion
        /// </summary>
        None,

        /// <summary>
        ///     Invert the Y and Z axis
        /// </summary>
        FlipYZ
    }

    /// <summary>
    ///     This exception is thrown by <see cref="IPublisherClient.StartTransaction"/> when a transaction is already pending.
    /// </summary>
    public class TransactionAlreadyPendingException : Exception
    { }

    /// <summary>
    ///     This exception is thrown by <see cref="IPublisherClient.StartTransaction"/> when a transaction is already pending.
    /// </summary>
    public class NoTransactionPendingException : Exception
    { }

    /// <summary>
    ///     This exception is thrown by <see cref="IPublisherClient.StartTransaction"/> when a transaction is already pending.
    /// </summary>
    public class SessionClosedException : Exception
    { }
}
