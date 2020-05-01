using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Logging.Loggers
{
    /// <summary>
    /// <see cref="IEventLogger"/> privides a common interface for basic logging.
    /// </summary>
    public interface IEventLogger : IDisposable
    {
        event EventHandler<EventLoggedEventArgs> EventLogged;

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message"></param>
        void Write(
            string message,
            [CallerFilePath]string callerPath = null,
            [CallerMemberName]string callerMemberName = null,
            [CallerLineNumber]int callerLineNumber = 0);

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message"></param>
        void Write(
            string message,
            Dictionary<string, string> data,
            [CallerFilePath]string callerPath = null,
            [CallerMemberName]string callerMemberName = null,
            [CallerLineNumber]int callerLineNumber = 0);

        /// <summary>
        /// Writes an exception to the log.
        /// </summary>
        /// <param name="ex"></param>
        void Write(
            Exception ex,
            [CallerFilePath]string callerPath = null,
            [CallerMemberName]string callerMemberName = null,
            [CallerLineNumber]int callerLineNumber = 0);

        /// <summary>
        /// Writes an exception to the log.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="data"></param>
        void Write(
            Exception ex,
            Dictionary<string, string> data,
            [CallerFilePath]string callerPath = null,
            [CallerMemberName]string callerMemberName = null,
            [CallerLineNumber]int callerLineNumber = 0);

        /// <summary>
        /// Writes a metric to the log.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Write(
            string name,
            double metric,
            [CallerFilePath]string callerPath = null,
            [CallerMemberName]string callerMemberName = null,
            [CallerLineNumber]int callerLineNumber = 0);
    }
}
