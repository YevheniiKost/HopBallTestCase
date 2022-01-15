using System;

namespace Core.Utilities

{
    public static class EventAggregator
    {
        public static void Subscribe<T>(Action<object, T> @event)
        {
            EventHolder<T>.Event += @event;
        }

        public static void Unsubscribe<T>(Action<object, T> @event)
        {
            EventHolder<T>.Event -= @event;
        }

        public static void Post<T>(object sender, T eventData)
        {
            EventHolder<T>.Post(sender, eventData);
        }

        private static class EventHolder<T>
        {
            public static event Action<object, T> Event;
            public static void Post(object sender, T eventData)
            {
                Event?.Invoke(sender, eventData);
            }
        }
    }
}
