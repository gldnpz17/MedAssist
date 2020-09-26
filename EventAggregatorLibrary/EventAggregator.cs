using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EventAggregatorLibrary
{
    public class EventAggregator : IEventAggregator
    {
        private List<AggregateEventBase> _aggregateEvents = new List<AggregateEventBase>();

        public T GetEvent<T>() where T : AggregateEventBase
        {
            //get instance
            List<AggregateEventBase> outputList = (from aggregateEvent
                                                   in _aggregateEvents
                                                   where aggregateEvent.GetType() == typeof(T)
                                                   select aggregateEvent).ToList();
            if (outputList.Count == 0)//if a singleton instance doesn't exist, create one
            {
                T instance = (T)Activator.CreateInstance(typeof(T));
                _aggregateEvents.Add(instance);

                return instance;
            }
            else
            {
                return (T)outputList.First();
            }
        }
    }
}
