using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Logging.Writers
{
    public sealed class FileWriter<T> : ConcurrentWriter<T>
    {
        private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        public string Path { get; }

        public FileWriter(string path)
            : base()
        {
            var folder = System.IO.Path.GetDirectoryName(path);
            if (!String.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            this.Path = path;
        }

        protected override bool CanListen()
        {
            return this.Path != null;
        }

        public override void Flush()
        {
            if (!this.Queue.IsEmpty)
            {
                this.locker.EnterWriteLock();
                try
                {
                    using var stream = new FileStream(this.Path, FileMode.Append, FileAccess.Write, FileShare.Read, 4096 * 128, FileOptions.SequentialScan | FileOptions.WriteThrough);
                    while (this.Queue.TryDequeue(out var item))
                    {
                        var data = Encoding.UTF8.GetBytes(item.ToString());
                        stream.Write(data, 0, data.Length);
                    }
                }
                finally
                {
                    this.locker.ExitWriteLock();
                }
            }
        }
    }
}
