using System;

namespace ColdWind.Core.EventLinks
{
    public interface IEventLink
    {
        public event Action Called;
    }

    public interface IEventLink<T>
    {
        public event Action<T> Called;
    }
}
