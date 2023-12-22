using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thresh.Core.Data;
using Thresh.Core.Variant;

namespace Thresh.Unity.Databind
{
    public delegate void MsgCallback(int msg_id, VariantList args);
    
    public class MsgBinder
    {
        private Dictionary<int, HashSet<MsgCallback>> _EventHandlers;

        public MsgBinder()
        {
            _EventHandlers = new Dictionary<int, HashSet<MsgCallback>>();
        }

        public void Callback(int msg_id, VariantList args)
        {
            HashSet<MsgCallback> found = null;
            if (!_EventHandlers.TryGetValue(msg_id, out found))
            {
                return;
            }
            HashSet<MsgCallback> tmpfound = new HashSet<MsgCallback>(found);

            foreach (var event_handler in tmpfound)
            {
                event_handler(msg_id, new VariantList(args));
            }
        }

        public void Register(int event_id, MsgCallback event_handler)
        {
            HashSet<MsgCallback> found = null;
            if (!_EventHandlers.TryGetValue(event_id,out found))
            {
                found = new HashSet<MsgCallback>();
                _EventHandlers.Add(event_id,found);
            }

            try
            {
                if (found.Contains(event_handler))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Cant Add Event Because {0} Has Exist in Event {1}",
                        event_handler.Method.Name, event_id.ToString());

                    throw new EventException(sb.ToString());
                }
                else
                {
                    found.Add(event_handler);
                }
            }
            catch (Exception e)
            {
                throw new EventException("EventException", e);
            }
        }

        public void Cancel(int event_id, MsgCallback event_handler)
        {
            HashSet<MsgCallback> found = null;
            if (!_EventHandlers.TryGetValue(event_id,out found))
            {
                return;
            }

            if (found.Contains(event_handler))
            {
                found.Remove(event_handler);
            }
        }
    }
}