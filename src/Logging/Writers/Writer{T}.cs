using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Logging.Writers
{
    public abstract class ConcurrentWriter<T> : IWriter<T>, IDisposable
    {
        private bool disposed = false;
        private CancellationTokenSource cancellationTokenSource = null;
        protected ConcurrentQueue<T> Queue { get; } = new ConcurrentQueue<T>();

        public ConcurrentWriter()
        {
            this.StartAsync();
        }

        public void Write(T e)
        {
            this.Queue.Enqueue(e);
        }

        protected async void StartAsync()
        {
            if (this.cancellationTokenSource == null)
            {
                this.cancellationTokenSource = new CancellationTokenSource();
                await Task.Run(() => this.Listen(this.cancellationTokenSource.Token));
            }
        }

        protected void Listen(CancellationToken cancellationToken)
        {
            var wait = default(SpinWait);
            while (!this.CanListen())
            {
                wait.SpinOnce();
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                this.Flush();
                wait.SpinOnce();
            }
        }

        public abstract void Flush();

        protected abstract bool CanListen();

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.cancellationTokenSource != null && !this.cancellationTokenSource.IsCancellationRequested)
                    {
                        this.cancellationTokenSource.Cancel(false);
                        this.cancellationTokenSource.Dispose();
                        this.cancellationTokenSource = null;
                    }

                    this.Flush();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
