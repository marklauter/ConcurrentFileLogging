using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Logging.Events
{
    public sealed class MetricEvent : ApplicationEvent
    {
        public MetricEvent(string name, double value, Dictionary<string, string> data, string callerPath, string callerMemberName, int callerLineNumber)
         : base(data, callerPath, callerMemberName, callerLineNumber)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("value")]
        public double Value { get; }
    }
}
