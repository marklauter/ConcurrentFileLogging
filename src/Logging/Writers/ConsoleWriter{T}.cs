using Logging.Events;
using System;

namespace Logging.Writers
{
    public sealed class ConsoleWriter<T> : ConcurrentWriter<T>
    {
        public ConsoleWriter()
            : base()
        {
        }

        public override void Flush()
        {
            while (this.Queue.TryDequeue(out var applicationEvent))
            {
                Console.ForegroundColor = applicationEvent switch
                {
                    ExceptionEvent _ => ConsoleColor.Red,
                    MetricEvent _ => ConsoleColor.Cyan,
                    _ => ConsoleColor.White,
                };
                Console.Write(applicationEvent);
                Console.ResetColor();
            }
        }

        protected override bool CanListen()
        {
            return true;
        }
    }
}
