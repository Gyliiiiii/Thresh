using System;
using System.Collections.Generic;
using System.Text;
using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Event
{
    public delegate void EventCallback(IKernel kernel, PersistID self, VariantList args);

    public class EventManager<EVENT_ID>
    {
        private Dictionary<EVENT_ID, HashSet<EventCallback>> _EventHandlers;

        public EventManager()
        {
            _EventHandlers = new Dictionary<EVENT_ID, HashSet<EventCallback>>();
        }

        public void Callback(EVENT_ID event_id, IKernel kernel, PersistID self, VariantList args)
        {
            HashSet<EventCallback> found = null;
            if (!_EventHandlers.TryGetValue(event_id, out found))
            {
                return;
            }

            foreach (var event_handler in found)
            {
                event_handler(kernel, self, new VariantList(args));
            }
        }

        public void Register(EVENT_ID event_id, EventCallback event_handler)
        {
            HashSet<EventCallback> found = null;
            if (!_EventHandlers.TryGetValue(event_id, out found))
            {
                found = new HashSet<EventCallback>();
                _EventHandlers.Add(event_id, found);
            }

            try
            {
                if (found.Contains(event_handler))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Cant Add Event Because {0} Has Exist in Event {1}", event_handler.Method.Name,
                        event_id.ToString());

                    throw new EventException(sb.ToString());
                }
                else
                {
                    found.Add(event_handler);
                }
            }
            catch (Exception e)
            {
                throw new EventException("EventException ", e);
            }
        }
    }
}