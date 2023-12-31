﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using ProtoBuf;
using Thresh.Core.Data;

namespace Thresh.Core.Variant
{
    public class VariantTypeCompare : IEqualityComparer<VariantType>
    {
        public bool Equals(VariantType x, VariantType y)
        {
            return x == y;
        }

        public int GetHashCode(VariantType obj)
        {
            return (int)obj;
        }
    }

    public interface IVariant
    {
        VariantType Type { get; }

        object ObjectValue { get; }

        string ToString();

        void Clear();
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantBoolean : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_BOOL_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_BOOL_VALUE)]
        public bool Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public VariantBoolean()
        {
            _Type = VariantType.Bool;
            Value = Constant.NULL_BOOLEAN;
        }

        public static implicit operator bool(VariantBoolean var)
        {
            if (var == null)
            {
                return Constant.NULL_BOOLEAN;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_BOOLEAN;
        }

        public static VariantBoolean New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantBoolean>(VariantType.Bool);
#else
            return new VariantBoolean();
#endif
        }

        public static VariantBoolean New(bool value)
        {
            VariantBoolean variant = New();
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantByte : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_BYTE_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_BYTE_VALUE)]
        public byte Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private byte _value;
        public int tt = 0;

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public VariantByte()
        {
            _Type = VariantType.Byte;
            Value = Constant.NULL_BYTE;
        }

        public static implicit operator byte(VariantByte var)
        {
            if (var == null)
            {
                return Constant.NULL_BYTE;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_BYTE;
        }

        public static VariantByte New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantByte>(VariantType.Byte);
#else
            return new VariantByte();
#endif
        }

        private static int t = 0;

        public static VariantByte New(byte value)
        {
            VariantByte variant = New();
            variant.Value = value;
            variant.tt = t++;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantInteger : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_INT_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_INT_VALUE)]
        public int Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

#if !UNITY_EDITOR && !BATTLE_SERVER
        ~VariantInteger()
        {
            VariantPool.Recycle<VariantInteger>(this);
        }
#endif

        public VariantInteger()
        {
            _Type = VariantType.Int;
            Value = Constant.NULL_INTEGER;
        }

        public static implicit operator int(VariantInteger var)
        {
            if (var == null)
            {
                return Constant.NULL_INTEGER;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_INTEGER;
        }

        public static VariantInteger New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantInteger>(VariantType.Int);
#else
            return new VariantInteger();
#endif
        }

        public static VariantInteger New(int value)
        {
            VariantInteger variant = New();
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantFloat : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_FLOAT_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_FLOAT_VALUE)]
        public float Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public VariantFloat()
        {
            _Type = VariantType.Float;
            Value = Constant.NULL_FLOAT;
        }

        public static implicit operator float(VariantFloat var)
        {
            if (var == null)
            {
                return Constant.NULL_FLOAT;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_FLOAT;
        }

        public static VariantFloat New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantFloat>(VariantType.Float);
#else
            return new VariantFloat();
#endif
        }

        public static VariantFloat New(float value)
        {
            VariantFloat variant = New();
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantLong : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_LONG_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_LONG_VALUE)]
        public long Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public VariantLong()
        {
            _Type = VariantType.Long;
            Value = Constant.NULL_LONG;
        }

        public static implicit operator long(VariantLong var)
        {
            if (var == null)
            {
                return Constant.NULL_LONG;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_LONG;
        }

        public static VariantLong New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantLong>(VariantType.Long);
#else
            return new VariantLong();
#endif
        }

        public static VariantLong New(long value)
        {
            VariantLong variant = New();
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantString : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_STRING_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_STRING_VALUE)]
        public string Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public VariantString()
        {
            _Type = VariantType.String;
            Value = Constant.NULL_STRING;
        }

        public static implicit operator string(VariantString var)
        {
            if (var == null)
            {
                return Constant.NULL_STRING;
            }

            return var.Value;
        }

        public void Clear()
        {
            Value = Constant.NULL_STRING;
        }

        public static VariantString New()
        {
#if UNITY
            return VariantPool.NewVariant<VariantString>(VariantType.String);
#else
            return new VariantString();
#endif
        }

        public static VariantString New(string value)
        {
            VariantString variant = New();
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantPersistID : IVariant
    {
        [DataMember] [ProtoMember(Constant.VARIANT_PROTO_PID_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_PID_VALUE)]
        public PersistID Value { get; set; }

        public VariantType Type
        {
            get { return _Type; }
        }

        public object ObjectValue
        {
            get { return Value; }
        }

        public int refcnt = 0;

        public VariantPersistID()
        {
            refcnt++;
            _Type = VariantType.PersistID;
            Value = PersistID.Empty;
        }

        public static implicit operator PersistID(VariantPersistID var)
        {
            if (var == null)
            {
                return PersistID.Empty;
            }

            return var.Value;
        }

        private string tmp22;

        public void Clear()
        {
            refcnt--;
            if (refcnt < 0)
            {
                int aa = 123;
            }

            tmp22 = new StackTrace(true).ToString();
            Value = PersistID.Empty;
        }

        public static VariantPersistID New()
        {
#if UNITY_64
            return VariantPool.NewVariant<VariantPersistID>(VariantType.PersistID);
#else
            return new VariantPersistID();
#endif
        }
        
        public static VariantPersistID New(PersistID value)
        {
            VariantPersistID variant = New();
            if (variant.refcnt > 0 )
            {
                int aa = 123;
            }
            variant.Value = value;
            return variant;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class VariantBytes : IVariant
    {
        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_BYTES_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_BYTES_VALUE)]

        public Bytes Value { get; set; }

        public VariantType Type { get { return _Type; } }

        public object ObjectValue { get { return Value; } }

        public VariantBytes(Bytes bytes)
        {
            _Type = VariantType.Bytes;
            Value = bytes;
        }

        public VariantBytes(byte[] bytes)
        {
            _Type = VariantType.Bytes;
            Value = Bytes.New(bytes);
        }
        
        public VariantBytes()
        {
            _Type = VariantType.Bytes;
            Value = Bytes.Zero;
        }
        public static explicit operator VariantBytes(byte[] bytes)
        {
            return new VariantBytes(bytes);
        }
        public static implicit operator Bytes(VariantBytes var)
        {
            if (var == null) { return Bytes.Zero; }
            return var.Value;
        }
        public void Clear()
        {
            Value = Bytes.Zero;
        }
        
        public static VariantBytes New()
        {
#if UNITY
            return VariantPool.NewVariant<VariantBytes>(VariantType.Bytes);
#else
            return new VariantBytes();
#endif
        }

        public static VariantBytes New(byte[] value)
        {
            Bytes bytes = Bytes.New(value);
            return New(bytes);
        }

        public static VariantBytes New(Bytes value)
        {
            VariantBytes variant = New();
            variant.Value = value;
            return variant;
        }
    }
    
    [DataContract]
    [Serializable]
    [ProtoContract]
    internal class VariantInt2 : IVariant
    {
        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_INT2_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_INT2_VALUE)]
        public Int2 Value { get; set; }

        public VariantType Type { get { return _Type; } }

        public object ObjectValue { get { return Value; } }

        public VariantInt2()
        {
            _Type = VariantType.Int2;
            Value = Int2.Zero;
        }

        public static implicit operator Int2(VariantInt2 var)
        {
            if (var == null) { return Int2.Zero; }
            return var.Value;
        }

        public void Clear()
        {
            Value = Int2.Zero;
        }

        public static VariantInt2 New()
        {
#if UNITY
            return VariantPool.NewVariant<VariantInt2>(VariantType.Int2);
#else
            return new VariantInt2();
#endif
        }

        public static VariantInt2 New(Int2 value)
        {
            VariantInt2 variant = New();
            variant.Value = value;
            return variant;
        }
    }
    
    [DataContract]
    [Serializable]
    [ProtoContract]
    internal class VariantInt3 : IVariant
    {
        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_INT3_TYPE)]
        private VariantType _Type;

        [DataMember]
        [ProtoMember(Constant.VARIANT_PROTO_INT3_VALUE)]
        public Int3 Value { get; set; }

        public VariantType Type { get { return _Type; } }

        public object ObjectValue { get { return Value; } }

        public VariantInt3()
        {
            _Type = VariantType.Int3;
            Value = Int3.Zero;
        }

        public static implicit operator Int3(VariantInt3 var)
        {
            if (var == null) { return Int3.Zero; }
            return var.Value;
        }

        public void Clear()
        {
            Value = Int3.Zero;
        }

        public static VariantInt3 New()
        {
#if UNITY
            return VariantPool.NewVariant<VariantInt3>(VariantType.Int3);
#else
            return new VariantInt3();
#endif
        }

        public static VariantInt3 New(Int3 value)
        {
            VariantInt3 variant = New();
            variant.Value = value;
            return variant;
        }
    }
}