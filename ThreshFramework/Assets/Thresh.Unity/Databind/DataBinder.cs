using System;
using System.Collections.Generic;
using System.Text;
using Thresh.Core.Data;
using Thresh.Core.Variant;
using Unity.VisualScripting;

namespace Thresh.Unity.Databind
{
    public delegate void DataCallback(PersistID self, VariantList args);
    
    public class DataBinder
    {
        private Dictionary<Group<DataType, PersistID, string>, HashSet<DataCallback>> _EventHandlers;
        private Dictionary<Group<DataType, PersistID>, HashSet<DataCallback>> _EventHandlersEx;

        public DataBinder()
        {
            _EventHandlers = new Dictionary<Group<DataType, PersistID, string>, HashSet<DataCallback>>();
            _EventHandlersEx = new Dictionary<Group<DataType, PersistID>, HashSet<DataCallback>>();
        }

        public void Callback(DataType data_type, PersistID pid, string name, VariantList args)
        {
            HashSet<DataCallback> found = null;
            Group<DataType, PersistID, string> event_id
                = new Group<DataType, PersistID, string>(data_type,pid,name);

            if (_EventHandlers.TryGetValue(event_id,out found))
            {
                HashSet<DataCallback> ttfound = new HashSet<DataCallback>(found);
                foreach (var event_handler in ttfound)
                {
                    event_handler(pid, new VariantList(args));
                }
            }

            Group<DataType, PersistID> event_idex
                = new Group<DataType, PersistID>(data_type, pid);
            if (_EventHandlersEx.TryGetValue(event_idex,out found))
            {
                HashSet<DataCallback> ttfound = new HashSet<DataCallback>(found);
                foreach (var event_handler in ttfound)
                {
                    event_handler(pid, new VariantList(args));
                }
            }
        }

        public void Register(DataType data_type, PersistID pid, string name, DataCallback event_handler)
        {
            Group<DataType, PersistID, string> event_id
                = new Group<DataType, PersistID, string>(data_type, pid, name);

            HashSet<DataCallback> found = null;
            if (!_EventHandlers.TryGetValue(event_id,out found))
            {
                found = new HashSet<DataCallback>();
                _EventHandlers.Add(event_id,found);
            }

            try
            {
                if (found.Contains(event_handler))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Cant Add Event Because {0} Has Exist in Event {1}", event_handler.Method.Name,
                        event_handler.ToString());

                    throw new EventException(sb.ToString());
                }
                else
                {
                    found.Add(event_handler);
                }
            }
            catch (Exception ex)
            {
                throw new EventException("EventException ", ex);
            }
        }

        public void UnRegister(DataType data_type, PersistID pid, string name, DataCallback event_handler)
        {
            Group<DataType, PersistID, string> event_id = new Group<DataType, PersistID, string>(data_type,pid,name);
            HashSet<DataCallback> found = null;
            try
            {
                if (!_EventHandlers.TryGetValue(event_id, out found))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Cant Del Event Because Event {0} Not Register", event_id.ToString());
                    throw new Exception(sb.ToString());
                }
                else
                {
                    if (!found.Contains(event_handler))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Cant Add Event Because {0} Not Exist int Event {1}",
                            event_handler.Method.Name, event_id.ToString());
                        throw new Exception(sb.ToString());
                    }
                    else
                    {
                        found.Remove(event_handler);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventException ", ex);
            }
        }

        public void Register(DataType data_type, PersistID pid, DataCallback event_handler)
        {
            Group<DataType, PersistID> event_id = new Group<DataType, PersistID>(data_type, pid);

            HashSet<DataCallback> found = null;
            if (!_EventHandlersEx.TryGetValue(event_id,out found))
            {
                found = new HashSet<DataCallback>();
                _EventHandlersEx.Add(event_id,found);
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
                throw new EventException("EventException ", e);
            }
        }

        public void UnRegister(DataType data_type, PersistID pid, DataCallback event_handler)
        {
            Group<DataType, PersistID> event_id = new Group<DataType, PersistID>(data_type, pid);
            HashSet<DataCallback> found = null;
            try
            {
                if (!_EventHandlersEx.TryGetValue(event_id, out found))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Cant Del Event Because Event {0} Not Register", event_id.ToString());
                    throw new Exception(sb.ToString());
                }
                else
                {
                    if (!found.Contains(event_handler))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Cant Add Event Because {0} Not Exist in Event {1}",
                            event_handler.Method.Name, event_id.ToString());
                    }
                    else
                    {
                        found.Remove(event_handler);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventException ", ex);
            }
        }
    }
}