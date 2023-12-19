using Thresh.Core.Config;
using Thresh.Core.Data;
using Thresh.Core.Event;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Kernel
{
    public class DataCallbackManager
    {
        private DataEventManager<PropertyEvent> _PropertyCallback = null;
        
        private DataEventManager<RecordEvent> _RecordCallback = null;
        
        private DataEventManager<ContainerEvent> _ContainerCallback = null;
        
        private EntityEventManager _EntityCallback = null;

        private IKernel _IKernel;

        public DataCallbackManager(IKernel kernel)
        {
            _IKernel = kernel;
            _PropertyCallback = new DataEventManager<PropertyEvent>();
            _RecordCallback = new DataEventManager<RecordEvent>();
            _ContainerCallback = new DataEventManager<ContainerEvent>();
            _EntityCallback = new EntityEventManager();
        }
        
        public void AddEntityCallback(string entity_type, EntityEvent entity_event, EventCallback event_handler)
        {
            _EntityCallback.Register(entity_type, entity_event, event_handler);
        }

        public void CallEntityHandler(PersistID pid, string entity_type, EntityEvent entity_event, VariantList args)
        {
            EntityDef entity_def = _IKernel.GetEntityDef(entity_type);

            if (entity_def != null && entity_def.InheritList != null) 
            {
                for (int i = entity_def.InheritList.Count - 1; i >=0; i--)
                {
                    string parent_type = entity_def.InheritList.StringAt(i);
                    
                    _EntityCallback.Callback(parent_type,entity_event,_IKernel,pid,args);
                }
            }

            if (entity_def != null)
            {
                _EntityCallback.Callback(entity_type,entity_event,_IKernel,pid,args);
            }
        }

        public void AddPropertyCallback(string entity_type, string property_name, PropertyEvent property_event,
            EventCallback event_handler)
        {
            _PropertyCallback.Register(entity_type,property_name,property_event,event_handler);
        }

        public void CallPropertyHanler(PersistID pid, string property_name, PropertyEvent property_event,
            VariantList args)
        {
            string entity_type = _IKernel.GetType(pid);

            EntityDef entity_def = _IKernel.GetEntityDef(entity_type);

            if (entity_def != null && entity_def.InheritList != null)
            {
                for (int i = entity_def.InheritList.Count - 1; i >= 0; i--)
                {
                    string parent_type = entity_def.InheritList.StringAt(i);

                    _PropertyCallback.Callback(property_event, parent_type, property_name, _IKernel, pid, args);
                }
            }

            if (entity_def != null)
            {
                _PropertyCallback.Callback(property_event, entity_type, property_name, _IKernel, pid, args);
            }
        }
        
         public void AddRecordCallback(string entity_type, string record_name, RecordEvent record_event,
            EventCallback event_handler)
        {
            _RecordCallback.Register(entity_type, record_name, record_event, event_handler);
        }

        public void CallRecordHandler(PersistID pid, string record_name, RecordEvent record_event, VariantList args)
        {
            string entity_type = _IKernel.GetType(pid);

            EntityDef entity_def = _IKernel.GetEntityDef(entity_type);

            if (entity_def != null && entity_def.InheritList != null)
            {
                for (int i = entity_def.InheritList.Count - 1; i >= 0; i--)
                {
                    string parent_type = entity_def.InheritList.StringAt(i);

                    _RecordCallback.Callback(record_event, parent_type, record_name, _IKernel, pid, args);
                }
            }
            
            if (entity_def != null)
            {
                _RecordCallback.Callback(record_event, entity_type, record_name, _IKernel, pid, args);
            }
        }

        public void AddContainerCallback(string entity_type, string container_name,
            ContainerEvent container_event, EventCallback event_handler)
        {
            _ContainerCallback.Register(entity_type, container_name, container_event, event_handler);
        }

        public void CallContainerHandler(PersistID pid, string container_name,
            ContainerEvent container_event, VariantList args)
        {
            string entity_type = _IKernel.GetType(pid);

            EntityDef entity_def = _IKernel.GetEntityDef(entity_type);

            if (entity_def != null && entity_def.InheritList != null)
            {
                for (int i = entity_def.InheritList.Count - 1; i >= 0; i--)
                {
                    string parent_type = entity_def.InheritList.StringAt(i);

                    _ContainerCallback.Callback(container_event, parent_type, container_name, _IKernel, pid, args);
                }
            }

            if (entity_def != null)
            {
                _ContainerCallback.Callback(container_event, entity_type, container_name, _IKernel, pid, args);
            }
        }
    }
}