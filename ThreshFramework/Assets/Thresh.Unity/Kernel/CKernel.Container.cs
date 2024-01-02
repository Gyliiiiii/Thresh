using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Unity.Kernel
{
    partial class CKernel
    {
         public bool FindChild(PersistID pid, string container_name, PersistID child)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return false;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return false;
            }

            return container.GetSlot(child) != Constant.INVALID_SLOT;
        }

        public bool FindContainer(PersistID pid, string container_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return false;
            }

            return entity.FindContainer(container_name);
        }

        public int GetCapacity(PersistID pid, string container_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.INVALID_SLOT;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return Constant.INVALID_SLOT;
            }

            return container.Capacity;
        }

        public VariantList GetChildren(PersistID pid, string container_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return VariantList.Empty;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return VariantList.Empty;
            }

            PersistID[] children = container.GetChildren();
            VariantList list = VariantList.New();

            foreach (PersistID child in children)
            {
                list.Append(child);
            }
            return list;
        }
        public int GetChildCnt(PersistID pid, string container_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return 0;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return 0;
            }
            return container.GetChildren().Length;
        }
        public void AddChild(PersistID pid, string container_name, PersistID child)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return;
            }

            VariantList result;
            if (!container.TryAddChild(child, out result))
            {
                return;
            }

            _DataCallbackManager.CallContainerHandler(pid, container_name, ContainerEvent.AddChild, result);

            if (entity.OnEntry)
            {
                CallbackContainerData(pid, container_name, ContainerEvent.AddChild, result);
            }
        }

        public void AddChild(PersistID pid, string container_name, int slot, PersistID child)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return;
            }

            VariantList result;
            if (!container.TryAddChild(slot, child, out result))
            {
                return;
            }

            _DataCallbackManager.CallContainerHandler(pid, container_name, ContainerEvent.AddChild, result);

            if (entity.OnEntry)
            {
                CallbackContainerData(pid, container_name, ContainerEvent.AddChild, result);
            }
        }

        public void DelChild(PersistID pid, string container_name, int slot)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return;
            }

            VariantList result;
            if (!container.TryDelChild(slot, out result))
            {
                return;
            }

            _DataCallbackManager.CallContainerHandler(pid, container_name, ContainerEvent.DelChild, result);

            if (entity.OnEntry)
            {
                CallbackContainerData(pid, container_name, ContainerEvent.DelChild, result);
            }
        }

        public void DelChild(PersistID pid, string container_name, PersistID child)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return;
            }

            VariantList result;
            if (!container.TryDelChild(child, out result))
            {
                return;
            }

            _DataCallbackManager.CallContainerHandler(pid, container_name, ContainerEvent.DelChild, result);

            if (entity.OnEntry)
            {
                CallbackContainerData(pid, container_name, ContainerEvent.DelChild, result);
            }
        }

        public int GetSlot(PersistID pid, string container_name, PersistID child)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.INVALID_SLOT;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return Constant.INVALID_SLOT;
            }

            return container.GetSlot(child);
        }

        public PersistID GetChild(PersistID pid, string container_name, int slot)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return PersistID.Empty;
            }

            IContainer container = entity.GetContainer(container_name);
            if (container == null)
            {
                return PersistID.Empty;
            }

            return container.GetChild(slot);
        }

        private void CallbackContainerData(PersistID pid, string container_name, 
            ContainerEvent container_event, VariantList result)
        {
            VariantList msg = VariantList.New();
            msg.Append(container_name).Append((byte)container_event).Append(result);
            _DataTypeCallbackManager.Callback(DataType.Container, this, pid, msg);
        }
        public PersistID GetRoot(PersistID pid)
        {
            return _EntityManager.GetRoot(pid);
        }
        public PersistID GetParent(PersistID pid)
        {
            return _EntityManager.GetParent(pid);
        }
    }
}