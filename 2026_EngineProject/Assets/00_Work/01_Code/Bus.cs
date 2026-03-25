using UnityEngine;

namespace Work.Code.Events
{
    public interface IEvent { }

    public static class Bus<T> where T : IEvent
    {
        public delegate void Event(T evt);

        public static Event OnEvent;
        public static void Raise(T evt) => OnEvent?.Invoke(evt);
    }
}