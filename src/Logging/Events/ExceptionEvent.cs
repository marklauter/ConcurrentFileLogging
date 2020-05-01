using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Logging.Events
{
    public sealed class ExceptionEvent : ApplicationEvent
    {
        public ExceptionEvent(Exception exception, Dictionary<string, string> data, string callerPath, string callerMemberName, int callerLineNumber)
            : base(data, callerPath, callerMemberName, callerLineNumber)
        {
            this.Exception = new ExceptionModel(exception);
        }

        [JsonProperty("exception")]
        public ExceptionModel Exception { get; }
    }
}
