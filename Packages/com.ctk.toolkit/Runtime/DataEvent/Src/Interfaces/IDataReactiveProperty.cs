using System;

namespace CTK.DataEvent
{
    public interface IDataReactiveProperty<T> : IDataEventHandler<T>, IDataEventInvoker<T>, IDataVariableProperty<T>, IDisposable
    {
    }

    public interface IDataReactiveProperty : IDataEventHandler, IDataEventInvoker, IDisposable
    {
    }
}
