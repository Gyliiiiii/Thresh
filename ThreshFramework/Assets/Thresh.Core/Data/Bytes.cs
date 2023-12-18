using System;
using System.Text;
using ProtoBuf;

namespace Thresh.Core.Data
{
    /// <summary>
    /// Represents a three dimensional mathematical vector.
    /// </summary>
    [Serializable]
    [ProtoContract]
    public struct Bytes : IComparable<Bytes>
    {
        public static readonly Bytes Zero = new Bytes();
        
        public static Bytes New() { return new Bytes(); }
        
        public static Bytes New(byte[] bytes) { return new Bytes(bytes); }
        
        [ProtoMember(1)]
        public byte[] ByteArray;
        
        public Bytes(byte[] bytes)
        {
            ByteArray = new byte[bytes.Length];
            bytes.CopyTo(ByteArray, 0);
        }
        
        public static Bytes Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Zero;
            }
            
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            
            return New(bytes);
        }
        
        public static implicit operator byte[] (Bytes bytes)
        {
            return bytes.ByteArray;
        }
        
        public static bool operator ==(Bytes left, Bytes right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(Bytes left, Bytes right)
        {
            return !left.Equals(right);
        }
        
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.ByteArray);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is byte[])
            {
                return ByteArray == obj;
            }
            
            return false;
        }
        
        public static int CompareBytes(byte[] left, byte[] right)
        {
            int result = 0;

            if (left.Length != right.Length)
            {
                result = left.Length - right.Length;
            }
            else
            {
                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] != right[i])
                    {
                        result = left[i] - right[i];
                        break;
                    }
                }
            }

            return result;
        }
        
        public int CompareTo(Bytes other)
        {
            return CompareBytes(ByteArray, other.ByteArray);
        }
    }
}