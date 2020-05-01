using Logging.Events;
using System;

namespace Logging.Loggers
{
    public class EventLoggedEventArgs : EventArgs
    {
        public EventLoggedEventArgs(ApplicationEvent applicationEvent)
        {
            this.ApplicationEvent = applicationEvent ?? throw new ArgumentNullException(nameof(applicationEvent));
        }

        public ApplicationEvent ApplicationEvent { get; }
    }
}
