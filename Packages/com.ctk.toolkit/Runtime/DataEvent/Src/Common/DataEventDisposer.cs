using System;

namespace CTK.DataEvent
{
    public class DataEventDisposer : IDisposable
    {
        private Action disposer;

        DataEventDisposer next = null;

        public DataEventDisposer(Action disposer)
        {
            this.disposer = disposer;
        }

        public DataEventDisposer SetNext(DataEventDisposer next)
        {
            this.next = next;
            return next;
        }

        public void Dispose()
        {
            disposer?.Invoke();
            disposer = null;

            next?.Dispose();
        }
    }
}
