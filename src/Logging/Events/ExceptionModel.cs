using Newtonsoft.Json;
using System;

namespace Logging.Events
{
    [JsonObject("exception")]
    public class ExceptionModel
    {
        public ExceptionModel(Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            this.Type = ex.GetType().Name;
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            if (ex.InnerException != null)
            {
                this.InnerException = new ExceptionModel(ex.InnerException);
            }
        }

        [JsonProperty("innerException")]
        public ExceptionModel InnerException { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("stackTrace")]
        public string StackTrace { get; }
    }
}
