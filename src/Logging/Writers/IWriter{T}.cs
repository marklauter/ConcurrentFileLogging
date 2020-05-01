namespace Logging.Writers
{
    public interface IWriter<T>
    {
        void Write(T applicationEvent);

        void Flush();
    }
}
