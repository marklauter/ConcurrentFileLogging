using Logging.Events;
using Logging.Writers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Logging.Loggers
{
    /// <summary>
    /// An implementation of <see cref="IEventLogger"/>
    /// </summary>
    public sealed class EventLogger : IEventLogger
    {
        private readonly FileWriter<ApplicationEvent> writer;

        public event EventHandler<EventLoggedEventArgs> EventLogged;

        public EventLogger(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.writer = new FileWriter<ApplicationEvent>(path);
        }

        /// <inheritdoc />
        public void Write(string message, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            this.Write(message, null, callerPath, callerMemberName, callerLineNumber);
        }

        /// <inheritdoc />
        public void Write(string message, Dictionary<string, string> data, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var applicationEvent = new MessageEvent(
                message,
                data,
                callerPath,
                callerMemberName,
                callerLineNumber);
            this.writer.Write(applicationEvent);
            EventLogged?.Invoke(this, new EventLoggedEventArgs(applicationEvent));
        }

        /// <inheritdoc />
        public void Write(Exception ex, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            this.Write(ex, null, callerPath, callerMemberName, callerLineNumber);
        }

        /// <inheritdoc />
        public void Write(Exception ex, Dictionary<string, string> data, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var applicationEvent = new ExceptionEvent(
                ex,
                data,
                callerPath,
                callerMemberName,
                callerLineNumber);
            this.writer.Write(applicationEvent);
            EventLogged?.Invoke(this, new EventLoggedEventArgs(applicationEvent));
        }

        /// <inheritdoc />
        public void Write(string name, double metric, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            var applicationEvent = new MetricEvent(
                name,
                metric,
                null,
                callerPath,
                callerMemberName,
                callerLineNumber);
            this.writer.Write(applicationEvent);
            EventLogged?.Invoke(this, new EventLoggedEventArgs(applicationEvent));
        }

        public void Dispose()
        {
            this.writer.Dispose();
        }
    }
}
