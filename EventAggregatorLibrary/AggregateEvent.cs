using System;
using System.Collections.Generic;
using System.Text;

namespace EventAggregatorLibrary
{
    public abstract class AggregateEvent<T> : AggregateEventBase
    {
        private event Action<T> EventPublished;

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="payload">The object sent along with the event.</param>
        public void Publish(T payload)
        {
            EventPublished?.Invoke(payload);
        }
        /// <summary>
        /// Subscribe to the event.
        /// </summary>
        /// <param name="action">The method handling the event.</param>
        public void Subscribe(Action<T> action)
        {
            EventPublished += action;
        }
    }
    public abstract class AggregateEvent : AggregateEventBase
    {
        private event Action EventPublished;

        /// <summary>
        /// Raises the event.
        /// </summary>
        public void Publish()
        {
            EventPublished?.Invoke();
        }
        /// <summary>
        /// Subscribe to the event.
        /// </summary>
        /// <param name="action">The method handling the event</param>
        public void Subscribe(Action action)
        {
            EventPublished += action;
        }
    }
}
