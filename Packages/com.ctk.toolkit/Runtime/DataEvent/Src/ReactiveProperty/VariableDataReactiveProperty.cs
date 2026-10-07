using System;
using UnityEngine.Events;

namespace CTK.DataEvent
{
    public class VariableDataReactiveProperty<T> : IDataReactiveProperty<T>
    {
        private readonly UnityEvent<T> condition = null;

        private readonly Func<T> getValue = null;

        private readonly Action<T> setValue = null;

        private readonly IDisposable disposer = null;

        private Action<T> callbacks = null;

        public T Value
        {
            get => getValue();
            set
            {
                setValue.Invoke(value);
            }
        }

        public VariableDataReactiveProperty(Func<T> getValue, Action<T> setValue)
        {
            this.getValue = getValue;
            this.setValue = setValue;
        }

        public VariableDataReactiveProperty(Func<T> getValue, Action<T> setValue, IDisposable disposer)
        {
            this.getValue = getValue;
            this.setValue = setValue;
            this.disposer = disposer;
        }

        public VariableDataReactiveProperty(UnityEvent<T> condition, Func<T> getValue, Action<T> setValue)
        {
            this.condition = condition;
            this.getValue = getValue;
            this.setValue = setValue;

            this.condition.AddListener(Invoke);
        }

        public VariableDataReactiveProperty(UnityEvent<T> condition, Func<T> getValue, Action<T> setValue, IDisposable disposer)
        {
            this.condition = condition;
            this.getValue = getValue;
            this.setValue = setValue;
            this.disposer = disposer;

            this.condition.AddListener(Invoke);
        }

        public IDisposable AddListener(Action<T> listener)
        {
            callbacks += listener;
            return new DataEventDisposer(() => callbacks -= listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            callbacks -= listener;
        }

        public void RemoveAllListener()
        {
            callbacks = null;
        }

        public void Invoke(T value)
        {
            callbacks?.Invoke(value);
        }

        public void Dispose()
        {
            callbacks = null;
            condition?.RemoveListener(Invoke);
            disposer?.Dispose();
        }
    }
}
