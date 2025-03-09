using System;

namespace ColdWind.Core.EventLinks
{
    public class InternalEventLink : IEventLink
    {
        public event Action Called;

        public void Invoke() => Called?.Invoke();
    }

    public class InternalEventLink<T> : IEventLink<T>
    {
        public event Action<T> Called;

        public void Invoke(T value) => Called?.Invoke(value);
    }
}
