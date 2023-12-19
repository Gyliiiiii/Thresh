using System;
using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Event
{
    public class DataEventManager<DATA_EVENT> : EventManager<Group<string,string,int>>
    {
        internal void Callback(DATA_EVENT event_enum, string entity_type, string name, IKernel kernel, PersistID self,
            VariantList args)
        {
            int event_id = (int)Convert.ChangeType(event_enum, TypeCode.Int32);

            Group<string, string, int> group = new Group<string, string, int>(entity_type, name, event_id);
            
            Callback(group, kernel, self, args);
        }

        internal void Register(string entity_type, string name, DATA_EVENT event_enum, EventCallback event_handler)
        {
            int event_id = (int)Convert.ChangeType(event_enum, TypeCode.Int32);

            Group<string, string, int> group = new Group<string, string, int>(entity_type, name, event_id);
            
            Register(group,event_handler);
        }
    }
}