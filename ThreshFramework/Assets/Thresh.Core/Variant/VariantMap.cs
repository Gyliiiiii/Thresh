using System;
using System.Collections.Generic;
using Thresh.Core.Data;

namespace Thresh.Core.Variant
{
    [Serializable]
    public class VariantMap
    {
        private Dictionary<string, IVariant> _VariantMap;

        public VariantMap()
        {
            _VariantMap = new Dictionary<string, IVariant>();
        }

        public IVariant this[string key]
        {
            get
            {
                return GetVariant(key);
            }
        }

        private IVariant GetVariant(string key)
        {
            IVariant found = null;
            _VariantMap.TryGetValue(key, out found);

            return found;
        }
        
        public int Count
        {
            get { return _VariantMap.Count; }
        }

        private void AddVariant(string key, IVariant value)
        {
            if (value == null)
            {
                return;
            }

            if (value.Type == VariantType.None)
            {
                return;
            }

            if (_VariantMap.ContainsKey(key))
            {
                return;
            }
            
            _VariantMap.Add(key,value);
        }
        
        public void Add(string key, bool value)      { AddVariant(key, new VariantBoolean() { Value = value }); }
        public void Add(string key, byte value)      { AddVariant(key, new VariantByte() { Value = value }); }
        public void Add(string key, int value)       { AddVariant(key, new VariantInteger() { Value = value }); }
        public void Add(string key, float value)     { AddVariant(key, new VariantFloat() { Value = value }); }
        public void Add(string key, long value)      { AddVariant(key, new VariantLong() { Value = value }); }
        public void Add(string key, string value)    { AddVariant(key, new VariantString() { Value = value }); }
        public void Add(string key, PersistID value) { AddVariant(key, new VariantPersistID() { Value = value }); }
        public void Add(string key, Bytes value)     { AddVariant(key, new VariantBytes() { Value = value }); }
        public void Add(string key, Int2 value)      { AddVariant(key, new VariantInt2() { Value = value }); }
        public void Add(string key, Int3 value)      { AddVariant(key, new VariantInt3() { Value = value }); }
        
        public bool      GetBool(string key)      { return GetVariant(key) as VariantBoolean; }
        public byte      GetByte(string key)      { return GetVariant(key) as VariantByte; }
        public float     GetFloat(string key)     { return GetVariant(key) as VariantFloat; }
        public int       GetInt(string key)       { return GetVariant(key) as VariantInteger; }
        public long      GetLong(string key)      { return GetVariant(key) as VariantLong; }
        public string    GetString(string key)    { return GetVariant(key) as VariantString; }
        public PersistID GetPid(string key)       { return GetVariant(key) as VariantPersistID; }
        public Bytes     GetBytes(string key)     { return GetVariant(key) as VariantBytes; }
        public Int2      GetInt2(string key)      { return GetVariant(key) as VariantInt2; }
        public Int3      GetInt3(string key)      { return GetVariant(key) as VariantInt3; }
        
    }
}