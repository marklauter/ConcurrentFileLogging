using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Logging.Events
{
    [JsonObject("event")]
    public abstract class ApplicationEvent
    {
        protected static readonly JsonSerializerSettings JsonFormatSetttings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include,
        };

        private string text;

        public ApplicationEvent(Dictionary<string, string> data, string callerPath, string callerMemberName, int callerLineNumber)
        {
            this.TimeStamp = DateTime.UtcNow;
            if (data != null)
            {
                this.Data = new ReadOnlyDictionary<string, string>(data);
            }

            this.CallerPath = Path.GetFileName(callerPath);
            this.CallerMemberName = callerMemberName;
            this.CallerLineNumber = callerLineNumber;
            this.EventType = this.GetType().Name;
        }

        [JsonProperty("sessionId")]
        public static Guid SessionId { get; } = Guid.NewGuid();

        [JsonProperty("type")]
        public string EventType { get; }

        [JsonProperty("callerPath")]
        public string CallerPath { get; }

        [JsonProperty("callerMemberName")]
        public string CallerMemberName { get; }

        [JsonProperty("callerLineNumber")]
        public int CallerLineNumber { get; }

        [JsonProperty("data")]
        public ReadOnlyDictionary<string, string> Data { get; }

        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; }

        protected virtual string GetText()
        {
            return $"{JsonConvert.SerializeObject(this, JsonFormatSetttings)}\n";
        }

        public override string ToString()
        {
            if (this.text == null)
            {
                this.text = this.GetText();
            }

            return this.text;
        }
    }
}
