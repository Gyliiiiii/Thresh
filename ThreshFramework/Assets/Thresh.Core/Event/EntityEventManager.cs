using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Event
{
    public class EntityEventManager : EventManager<Group<string, EntityEvent>>
    {
        internal void Callback(string entity_type, EntityEvent event_enum, IKernel kernel, PersistID self,
            VariantList args)
        {
            Group<string, EntityEvent> group = new Group<string, EntityEvent>(entity_type, event_enum);

            Callback(group, kernel, self, args);
        }

        internal void Register(string entity_type, EntityEvent event_enum, EventCallback event_handler)
        {
            Group<string, EntityEvent> group = new Group<string, EntityEvent>(entity_type, event_enum);
            
            Register(group,event_handler);
        }
    }
}

