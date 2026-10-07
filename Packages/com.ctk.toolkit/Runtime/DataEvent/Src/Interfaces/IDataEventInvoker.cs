namespace CTK.DataEvent
{
    public interface IDataEventInvoker<T>
    {
        void Invoke(T val);
    }

    public interface IDataEventInvoker
    {
        void Invoke();
    }
}
