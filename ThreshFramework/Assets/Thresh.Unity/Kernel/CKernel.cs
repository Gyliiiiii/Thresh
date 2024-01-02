using System;
using System.Collections.Generic;
using Thresh.Core.Config;
using Thresh.Core.Data;
using Thresh.Core.Event;
using Thresh.Core.Interface;
using Thresh.Core.Kernel;
using Thresh.Core.Utility;
using Thresh.Core.Variant;
using Thresh.Unity.Global;

namespace Thresh.Unity.Kernel
{
    public partial class CKernel : IKernel
    {
        private EntityManager          _EntityManager                    = null;
        private DataCallbackManager    _DataCallbackManager              = null;
        private ModuleManager          _ModuleManager                    = null;
        private TimerManager           _TimerManager                     = null;
        private EventManager<int>      _CommandMsgCallbackManager        = null;
        private EventManager<int>      _CustomMsgCallbackManager         = null;
        private EventManager<DataType> _DataTypeCallbackManager          = null;
        private ILog                   _Logger                           = null;


        public CKernel(Definition define)
        {
            _EntityManager = new EntityManager(this);
            _DataCallbackManager = new DataCallbackManager(this);
            _ModuleManager = new ModuleManager(this);
            _TimerManager = new TimerManager(this);
            _CommandMsgCallbackManager = new EventManager<int>();
            _CustomMsgCallbackManager = new EventManager<int>();
            _DataTypeCallbackManager = new EventManager<DataType>();
            _Logger = LogAssert.GetLog("Kernel");
            _Define = define;

        }

        private Definition _Define = null;
        public Definition Define { get { return _Define; } }

        public PersistID CreateEntity(string define, PersistID root, VariantList args = null)
        {
            IEntity entity = _EntityManager.CreateEntity(define, root);
            if (entity == null)
            {
                return PersistID.Empty;
            }
            
            VariantList new_args = VariantList.New();
            if (args != null) new_args.Append(args);
            _DataCallbackManager.CallEntityHandler(entity.Self,entity.Type,EntityEvent.OnCreate,new_args);

            return entity.Self;
        }

        public PersistID CreateEntity(PersistID pid, string define, VariantList args = null)
        {
            throw new NotImplementedException();
        }

        public PersistID CreateEntity(PersistID pid, string define, PersistID root, VariantList args = null)
        {
            IEntity entity = _EntityManager.CreateEntity(pid, define);
            if (entity == null)
            {
                return PersistID.Empty;
            }

            VariantList new_args = VariantList.New();
            if (args != null) new_args.Append(args);
            _DataCallbackManager.CallEntityHandler(entity.Self,entity.Type,EntityEvent.OnCreate,new_args);

            return entity.Self;
        }
        
        
        public EntityDef GetEntityDef(string type)
        {
            return _Define.GetEntity(type);
        }

        public void CreateModule<T>() where T : IModule
        {
            _ModuleManager.CreateModule<T>();
        }

        public void LoadEntity(PersistID pid, VariantList entity_protos)
        {
            for (int i = 0; i < entity_protos.Count; i++)
            {
                byte[] entity_proto = entity_protos.BytesAt(i);
                Entity entity = DataUtil.BytesToObject<Entity>(entity_proto);

                if (_EntityManager.FindEntity(entity.Self))
                {
                    continue;
                }
                
                _EntityManager.AddEntity(entity);
                
                _DataCallbackManager.CallEntityHandler(entity.Self,entity.Type,EntityEvent.OnLoad,VariantList.Empty);
            }
        }

        public bool Equals(PersistID pid, string type)
        {
            return type.Equals(GetType(pid));
        }

        public void EntryEntity(PersistID root)
        {
            IEntity entity = _EntityManager.GetEntity(root);
            if (entity == null)
            {
                return;
            }

            entity.OnEntry = true;

            foreach (var container in entity.GetContainers())
            {
                PersistID[] children = container.GetChildren();
                for (int i = 0; i < children.Length; i++)
                {
                    EntryEntity(children[i]);
                }
            }
        }

        public bool IsExist(PersistID pid)
        {
            return _EntityManager.FindEntity(pid);
        }

        public string GetType(PersistID pid)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_STRING;
            }

            return entity.Type;
        }


        public void DestroyEntity(PersistID pid)
        {
            _EntityManager.DelEntity(pid);
        }

        public VariantList ToProto(PersistID pid)
        {
            VariantList entity_bytes = VariantList.New();
            
            ParseEntity(pid,ref entity_bytes);

            return entity_bytes;
        }

        private void ParseEntity(PersistID pid, ref VariantList proto)
        {
            Entity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            byte[] entity_bytes = DataUtil.ObjectToBytes(entity);
            if (entity_bytes == null || entity_bytes.Length == 0)
            {
                return;
            }

            proto.Append(entity_bytes);

            foreach (var container in entity.GetContainers())
            {
                foreach (var child_pid in container.GetChildren())
                {
                    ParseEntity(child_pid,ref proto);
                }
            }
        }

        public void AddCountdown(PersistID pid, string countdown_name, long over_millseconds, TimerCallback countdown)
        {
            _TimerManager.AddTimer(pid,countdown_name,over_millseconds,countdown);
        }

        public void DelCountdown(PersistID pid, string countdown_name)
        {
            _TimerManager.DelTimer(pid,countdown_name);
        }

        public void AddHeartbeat(PersistID pid, string heartbeat_name, long gap_millseconds, int count, TimerCallback heartbeat)
        {
            _TimerManager.AddTimer(pid,heartbeat_name,gap_millseconds,count,heartbeat);
        }

        public void DelHeartbeat(PersistID pid, string heartbeat_name)
        {
            _TimerManager.DelTimer(pid,heartbeat_name);
        }

        public Entity[] GetEntities()
        {
            List<Entity> list = _EntityManager.GetEntitys();
            return list.ToArray();
        }
    }
}