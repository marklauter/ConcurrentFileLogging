using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Logging.Events
{
    public sealed class MessageEvent : ApplicationEvent
    {
        public MessageEvent(string message, Dictionary<string, string> data, string callerPath, string callerMemberName, int callerLineNumber)
            : base(data, callerPath, callerMemberName, callerLineNumber)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        [JsonProperty("message")]
        public string Message { get; }
    }
}
