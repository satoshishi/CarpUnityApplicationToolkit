using System;

namespace CTK.DataEvent
{
    public interface IDataEventHandler<T>
    {
        IDisposable AddListener(Action<T> listener);

        void RemoveListener(Action<T> listener);

        void RemoveAllListener();
    }

    public interface IDataEventHandler
    {
        IDisposable AddListener(Action listener);

        void RemoveListener(Action listener);

        void RemoveAllListener();
    }
}
