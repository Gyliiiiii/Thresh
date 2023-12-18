using System.Collections.Generic;
using ProtoBuf;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Data
{
    [ProtoContract]
    public class Container : IContainer
    {
        [ProtoMember(Constant.DATA_PROTO_CONTAINER_NAME)]
        private string _Name;

        public string Name
        {
            get { return _Name; }
        }

        [ProtoMember(Constant.DATA_PROTO_CONTAINER_CAPACITY)]
        private int _Capacity = 0;

        public int Capacity
        {
            get { return _Capacity; }
        }

        private int _FreeSlot = Constant.INVALID_SLOT;

        public int FreeSlot
        {
            get
            {
                if (IsFree(_FreeSlot))
                {
                    return _FreeSlot;
                }

                _FreeSlot = Constant.INVALID_SLOT;

                for (int index = 0; index < Capacity; index++)
                {
                    if (IsFree(index))
                    {
                        _FreeSlot = index;
                        break;
                    }
                }

                return _FreeSlot;
            }
        }

        public PersistID[] GetChildren()
        {
            PersistID[] found = new PersistID[_SlotDic.Values.Count];
            _SlotDic.Values.CopyTo(found,0);

            return found;
        }

        [ProtoMember(Constant.DATA_PROTO_CONTAINER_SLOTS)]
        private Dictionary<int, PersistID> _SlotDic;
        [ProtoMember(Constant.DATA_PROTO_CONTAINER_PIDS)]
        private Dictionary<PersistID, int> _PidDic;


        public Container()
        {
            _SlotDic = new Dictionary<int, PersistID>();
            _PidDic = new Dictionary<PersistID, int>();
        }

        public Container(string name, int capacity) : this()
        {
            _Name = name;
            _Capacity = capacity;
        }


        private bool IsFree(int slot)
        {
            if (slot < 0 || slot >= Capacity)
            {
                return false;
            }

            return !_SlotDic.ContainsKey(slot);
        }

        private bool IsSlot(PersistID pid)
        {
            return _PidDic.ContainsKey(pid);
        }

        public PersistID GetChild(int slot)
        {
            PersistID persist_id = PersistID.Empty;
            _SlotDic.TryGetValue(slot, out persist_id);

            return persist_id;
        }

        public int GetSlot(PersistID child)
        {
            int slot = Constant.INVALID_SLOT;
            _PidDic.TryGetValue(child, out slot);

            return slot;
        }

        public bool TryAddChild(PersistID pid, out VariantList result)
        {
            return TryAddChild(FreeSlot, pid, out result);
        }

        public bool TryAddChild(int slot, PersistID pid, out VariantList result)
        {
            result = null;

            if (!IsFree(slot))
            {
                return false;
            }else if (IsSlot(pid))
            {
                return false;
            }
            else
            {
                _SlotDic.Add(slot,pid);
                _PidDic.Add(pid,slot);

                result = VariantList.New();
                result.Append(slot);
                result.Append(pid);
                return true;
            }
        }

        public bool TryDelChild(PersistID pid, out VariantList result)
        {
            result = null;

            int slot = GetSlot(pid);
            if (slot == Constant.INVALID_SLOT)
            {
                result = null;
                return false;
            }
            else
            {
                _SlotDic.Remove(slot);
                _PidDic.Remove(pid);
                result = VariantList.New();
                result.Append(slot);
                result.Append(pid);
                
                return true;
            }
        }

        public bool TryDelChild(int slot, out VariantList result)
        {
            result = null;

            PersistID pid = GetChild(slot);

            return TryDelChild(pid,out result);
        }

        public void Clear()
        {
            _SlotDic.Clear();
            _PidDic.Clear();
            _FreeSlot = Constant.INVALID_SLOT;
        }
    }
}