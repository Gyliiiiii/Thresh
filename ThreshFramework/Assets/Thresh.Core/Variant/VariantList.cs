using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Thresh.Core.Data;

namespace Thresh.Core.Variant
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantList
    {
        [DataMember] [ProtoMember(1, OverwriteList = true)]
        private List<IVariant> _VariantList;

        public int Count
        {
            get { return _VariantList.Count; }
        }

        public static readonly VariantList Empty = new VariantList();

        public static VariantList New()
        {
#if UNITY_64
            return VariantPool.NewList();
#else
                return new VariantList();
#endif
        }

        public VariantList()
        {
            _VariantList = new List<IVariant>();
        }

        public VariantList(VariantList list)
        {
            _VariantList = new List<IVariant>();
            _VariantList.AddRange(list._VariantList);
        }

        public VariantList(IEnumerable<IVariant> collection)
        {
            _VariantList = new List<IVariant>(collection);
        }

        public IVariant this[int index]
        {
            get { return GetVariant(index); }
        }

        private IVariant GetVariant(int index)
        {
            if (index < 0 || index >= Count)
            {
                return null;
            }

            return _VariantList[index];
        }

        public bool BoolAt(int index)
        {
            return GetVariant(index) as VariantBoolean;
        }

        public byte ByteAt(int index)
        {
            return GetVariant(index) as VariantByte;
        }

        public int IntAt(int index)
        {
            return GetVariant(index) as VariantInteger;
        }

        public long LongAt(int index)
        {
            return GetVariant(index) as VariantLong;
        }

        public float FloatAt(int index)
        {
            return GetVariant(index) as VariantFloat;
        }

        public string StringAt(int index)
        {
            return GetVariant(index) as VariantString;
        }

        public PersistID PidAt(int index)
        {
            return GetVariant(index) as VariantPersistID;
        }

        public Bytes BytesAt(int index)
        {
            return GetVariant(index) as VariantBytes;
        }

        public Int2 Int2At(int index)
        {
            return GetVariant(index) as VariantInt2;
        }

        public Int3 Int3At(int index)
        {
            return GetVariant(index) as VariantInt3;
        }

        public void RemoveRange(int index, int n, bool pool = true)
        {
            if (index >= _VariantList.Count || index < 0)
            {
                return;
            }
#if UNITY_64
            if (pool)
            {
                for (int i = index; i < index + n && i < _VariantList.Count; i++)
                {
                    IVariant variant = _VariantList[i];
                    VariantPool.Recycle(variant);
                }
            }
#endif

            _VariantList.RemoveRange(index, n);
        }

        public void Remove(int index, bool pool = true)
        {
            if (index >= _VariantList.Count || index < 0)
            {
                return;
            }

            IVariant variant = _VariantList[index];
#if UNITY_64
            if (pool)
            {
                VariantPool.Recycle(variant);
            }
#endif
            _VariantList.RemoveAt(index);
        }

        public void Clear()
        {
            _VariantList.Clear();
        }

        public VariantList Append(VariantList value)
        {
            for (int i = 0; i < value.Count; i++)
            {
                switch (value[i].Type)
                {
                    case VariantType.Bool:
                        Append((value[i] as VariantBoolean).Value);
                        break;
                    case VariantType.Byte:
                        Append((value[i] as VariantByte).Value);
                        break;
                    case VariantType.Int:
                        Append((value[i] as VariantInteger).Value);
                        break;
                    case VariantType.Float:
                        Append((value[i] as VariantFloat).Value);
                        break;
                    case VariantType.Long:
                        Append((value[i] as VariantLong).Value);
                        break;
                    case VariantType.String:
                        Append((value[i] as VariantString).Value);
                        break;
                    case VariantType.PersistID:
                        Append((value[i] as VariantPersistID).Value);
                        break;
                    case VariantType.Bytes:
                        Append((value[i] as VariantBytes).Value);
                        break;
                    case VariantType.Int2:
                        Append((value[i] as VariantInt2).Value);
                        break;
                }
            }

            return this;
        }
        
        public VariantList Append(bool value)      { _VariantList.Add(VariantBoolean.New(value)); return this; }
        public VariantList Append(byte value)      { _VariantList.Add(VariantByte.New(value)); return this; }
        public VariantList Append(int value)       { _VariantList.Add(VariantInteger.New(value)); return this; }
        public VariantList AppendInt(int value)       { _VariantList.Add(VariantInteger.New(value)); return this; }
        public VariantList Append(float value)     { _VariantList.Add(VariantFloat.New(value)); return this; }
        public VariantList Append(long value)      { _VariantList.Add(VariantLong.New(value)); return this; }
        public VariantList Append(string value)    { _VariantList.Add(VariantString.New(value)); return this; }
        public VariantList Append(PersistID value) { _VariantList.Add(VariantPersistID.New(value)); return this; }
        public VariantList Append(Bytes value)     { _VariantList.Add(VariantBytes.New(value)); return this; }
        public VariantList Append(byte[] value)    { _VariantList.Add(VariantBytes.New(value)); return this; }
        public VariantList Append(Int2 value)      { _VariantList.Add(VariantInt2.New(value)); return this; }
        public VariantList Append(Int3 value)      { _VariantList.Add(VariantInt3.New(value)); return this; }
        public VariantList Append(IVariant value)    { _VariantList.Add(value); return this; }

        private bool Check(int index, VariantType type)
        {
            if (index >= _VariantList.Count)
            {
                return false;
            }

            return _VariantList[index].Type == type;
        }

        public void SetBool(int index, bool value)
        {
            if (!Check(index, VariantType.Bool))
            {
                return;
            }
            (_VariantList[index] as VariantBoolean).Value = value;
        }
        
          public void SetByte(int index, byte value)
        {
            if (!Check(index, VariantType.Byte))
            {
                return;
            }

            (_VariantList[index] as VariantByte).Value = value;
        }

        public void SetInt(int index, int value)
        {
            if (!Check(index, VariantType.Int))
            {
                return;
            }

            (_VariantList[index] as VariantInteger).Value = value;
        }

        public void SetLong(int index, long value)
        {
            if (!Check(index, VariantType.Long))
            {
                return;
            }

            (_VariantList[index] as VariantLong).Value = value;
        }

        public void SetFloat(int index, float value)
        {
            if (!Check(index, VariantType.Float))
            {
                return;
            }

            (_VariantList[index] as VariantFloat).Value = value;
        }

        public void SetString(int index, string value)
        {
            if (!Check(index, VariantType.String))
            {
                return;
            }

            (_VariantList[index] as VariantString).Value = value;
        }

        public void SetPersistID(int index, PersistID value)
        {
            if (!Check(index, VariantType.PersistID))
            {
                return;
            }

            (_VariantList[index] as VariantPersistID).Value = value;
        }

        public void SetBytes(int index, Bytes value)
        {
            if (!Check(index, VariantType.Bytes))
            {
                return;
            }

            (_VariantList[index] as VariantBytes).Value = value;
        }
        public void SetBytes(int index, byte[] value)
        {
            if (!Check(index, VariantType.Bytes))
            {
                return;
            }

            (_VariantList[index] as VariantBytes).Value = VariantBytes.New(value);
        }

        public void SetInt2(int index, Int2 value)
        {
            if (!Check(index, VariantType.Int2))
            {
                return;
            }

            (_VariantList[index] as VariantInt2).Value = value;
        }

        public void SetInt3(int index, Int3 value)
        {
            if (!Check(index, VariantType.Int3))
            {
                return;
            }

            (_VariantList[index] as VariantInt3).Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{VariantList");
            for (int i = 0; i < Count; i++)
            {
                sb.AppendFormat("({0}:{1})", i, _VariantList[i].ObjectValue);
            }

            sb.Append('}');
            return sb.ToString();
        }

        public bool SubtractBool(int index)
        {
            bool value = BoolAt(index);
            Remove(index);
            return value;
        }
        
        public byte SubtractByte(int index)
        {
            byte value = ByteAt(index);
            Remove(index);
            return value;
        }
        
        public int SubtractInt(int index)
        {
            int value = IntAt(index);
            Remove(index);
            return value;
        }

        public float SubtractFloat(int index)
        {
            float value = FloatAt(index);
            Remove(index);
            return value;
        }

        public long SubtractLong(int index)
        {
            long value = LongAt(index);
            Remove(index);
            return value;
        }

        public string SubtractString(int index)
        {
            string value = StringAt(index);
            Remove(index);
            return value;
        }

        public IVariant SubtractVariant(int index)
        {
            IVariant value = GetVariant(index);
            Remove(index,false);
            return value;
        }

        public PersistID SubtractPid(int index)
        {
            PersistID value = PidAt(index);
            Remove(index);
            return value;
        }

        public Bytes SubtractBytes(int index)
        {
            Bytes value = BytesAt(index);
            Remove(index);
            return value;
        }

        public Int2 SubtractInt2(int index)
        {
            Int2 value = Int2At(index);
            Remove(index);
            return value;
        }

        public Int3 SubtractInt3(int index)
        {
            Int3 value = Int3At(index);
            Remove(index);
            return value;
        }

        public VariantList SubtractRange(int index, int count,bool pool = true)
        {
            var result = new VariantList(_VariantList.GetRange(index, count));
            RemoveRange(index, count, pool);
            return result;
        }

        public bool      SubtractBool()      { return SubtractBool(0); }
        public byte      SubtractByte()      { return SubtractByte(0); }
        public int       SubtractInt()       { return SubtractInt(0); }
        public float     SubtractFloat()     { return SubtractFloat(0); }
        public long      SubtractLong()      { return SubtractLong(0); }
        public string    SubtractString()    { return SubtractString(0); }
        public IVariant    SubtractVariant()    { return SubtractVariant(0); }
        public PersistID SubtractPid()       { return SubtractPid(0); }
        public Bytes     SubtractBytes()     { return SubtractBytes(0); }
        public Int2      SubtractInt2()      { return SubtractInt2(0); }
        public Int3      SubtractInt3()      { return SubtractInt3(0); }

        public VariantList Copy()
        {
            return new VariantList(this);
        }
        
        public void Recycle()
        {
#if UNITY
            VariantPool.Recycle(this);
#else
            Clear();
#endif

        }

    }
}