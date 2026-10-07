using System;
using UnityEngine.Events;

namespace CTK.DataEvent
{
    public class NonVariableDataReactiveProperty : IDataReactiveProperty
    {
        private readonly UnityEvent condition = null;

        private readonly IDisposable disposer = null;

        private Action callbacks = null;

        public NonVariableDataReactiveProperty(UnityEvent condition)
        {
            this.condition = condition;

            this.condition.AddListener(Invoke);
        }

        public NonVariableDataReactiveProperty(UnityEvent condition, IDisposable disposer)
        {
            this.condition = condition;
            this.disposer = disposer;

            this.condition.AddListener(Invoke);
        }

        public IDisposable AddListener(Action listener)
        {
            callbacks += listener;
            return new DataEventDisposer(() => callbacks -= listener);
        }

        public void RemoveListener(Action listener)
        {
            callbacks -= listener;
        }

        public void RemoveAllListener()
        {
            callbacks = null;
        }

        public void Invoke()
        {
            callbacks?.Invoke();
        }

        public void Dispose()
        {
            callbacks = null;
            condition.RemoveListener(Invoke);
            disposer?.Dispose();
        }
    }
}
