
using System.Collections.Generic;
using Thresh.Core.Config;
using Thresh.Core.Data;
using Thresh.Core.Interface;
using Unity.VisualScripting;

namespace Thresh.Core.Kernel
{
    public class EntityManager
    {
        private Dictionary<PersistID, Entity> _EntityDic;

        private Entity _Entity;

        private IKernel _Kernel;
        private Dictionary<PersistID, PersistID> _RootDic;
        private Dictionary<PersistID, PersistID> _ParentDic;

        public EntityManager(IKernel kernel)
        {
            _Kernel = kernel;
            _EntityDic = new Dictionary<PersistID, Entity>(new PersistIDCompare());
            _RootDic = new Dictionary<PersistID, PersistID>(new PersistIDCompare());
            _ParentDic = new Dictionary<PersistID, PersistID>(new PersistIDCompare());
            _Entity = null;
        }

        #region ----- ----- ----- ----- Entity ----- ----- ----- -----

        public Entity GetEntity(PersistID pid)
        {
            if (_Entity != null && _Entity.Self == pid)
            {
                return _Entity;
            }

            _EntityDic.TryGetValue(pid, out _Entity);
            return _Entity;
        }

        public List<Entity> GetEntitys()
        {
            List<Entity> list = new List<Entity>();
            if (_EntityDic != null)
            {
                foreach (var key in _EntityDic.Keys)
                {
                    Entity en = null;
                    if (_EntityDic.TryGetValue(key,out en))
                    {
                        list.Add(en);
                    }
                }
            }

            return list;
        }

        public void AddEntity(Entity entity)
        {
            if (_Entity != null && _EntityDic.ContainsKey(entity.Self))
            {
                return;
            }
            AddRootInfo(entity);
            _EntityDic.Add(entity.Self,entity);
        }

        public bool FindEntity(PersistID pid)
        {
            if (_Entity != null && _Entity.Self == pid)
            {
                return true;
            }

            return _EntityDic.ContainsKey(pid);
        }

        private Entity GenEntity(string name)
        {
            EntityDef definition = _Kernel.getEntityDef(name);
            if (definition == null)
            {
                return null;
            }

            Entity new_entity = new Entity();
            new_entity.Type = definition.Name;

            foreach (var property_def in definition.GetAllProperties())
            {
                byte type = property_def.Define.GetByte(Constant.FLAG_TYPE);
                new_entity.createProperty(property_def.Name, (VariantType)type);
            }
            
            foreach (RecordDef record_def in definition.GetAllRecords())
            {
                int rows = record_def.Define.GetInt(Constant.FLAG_ROWS);

                new_entity.CreateRecord(record_def.Name, rows, record_def.ColTypes);
            }

            foreach (ContainerDef container_def in definition.GetAllContainers())
            {
                int capacity = container_def.Define.GetInt(Constant.FLAG_CAPACITY);

                new_entity.CreateContainer(container_def.Name, capacity);
            }

            return new_entity;
        }

        public IEntity CreateEntity(PersistID pid, string name)
        {
            if (PersistID.IsEmpty(pid))
            {
                return null;
            }

            if (_EntityDic.ContainsKey(pid))
            {
                return null;
            }

            Entity new_entity = GenEntity(name);
            if (new_entity == null)
            {
                return null;
            }

            AddRootInfo(new_entity);
            _EntityDic.Add(pid,new_entity);
            new_entity.Self = pid;

            return new_entity;
        }

        public IEntity CreateEntity(string name, PersistID root)
        {
            PersistID pid = root.Spawn();
            return CreateEntity(pid, name);
        }

        public bool DelEntity(PersistID pid)
        {
            Entity found = null;
            if (_EntityDic.TryGetValue(pid,out found))
            {
                found.Clear();
            }

            return _EntityDic.Remove(pid);
        }

        public PersistID GetRoot(PersistID pid)
        {
            PersistID ret = PersistID.Empty;
            if (_RootDic.TryGetValue(pid, out ret))
            {
                return ret;
            }
            PersistID tt = pid;
            while (true)
            {
                if (_ParentDic.TryGetValue(pid,out ret))
                {
                    pid = ret;
                }
                else
                {
                    _RootDic[tt] = pid;
                    return pid;
                }
            }            
        }

        public PersistID GetParent(PersistID pid)
        {
            PersistID ret = PersistID.Empty;
            if (_ParentDic.TryGetValue(pid,out ret))
            {
                return ret;
            }

            return PersistID.Empty;
        }
        
        private void AddRootInfo(Entity entity)
        {
            var cons = entity.GetContainers();
            for (int i = 0; i < cons.Length; i++)
            {
                var con = cons[i];
                var pids = con.GetChildren();
                for (int j = 0; j < pids.Length; j++)
                {
                    _ParentDic[pids[j]] = entity.Self;
                }
            }
        }

        public void AddParentInfo(PersistID node, PersistID parent)
        {
            _ParentDic[node] = parent;
            _RootDic.Remove(node);
        }

        public void RemoveParentInfo(PersistID node)
        {
            _ParentDic.Remove(node);
            _RootDic.Remove(node);
        }
        #endregion
    }
}
