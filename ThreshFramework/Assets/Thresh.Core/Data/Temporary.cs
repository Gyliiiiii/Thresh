using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Core.Data
{
    public class Temporary : ITemporary
    {
        private IVariant _Variant;

        private string _Name;
        public string Name { get { return _Name; } }
        
        internal Temporary(string name, VariantType type)
        {
            _Name = name;

            switch (type)
            {
                case VariantType.Bool:
                    _Variant = VariantBoolean.New();
                    break;
                case VariantType.Byte:
                    _Variant = VariantByte.New();
                    break;
                case VariantType.Int:
                    _Variant = VariantInteger.New();
                    break;
                case VariantType.Long:
                    _Variant = VariantLong.New();
                    break;
                case VariantType.Float:
                    _Variant = VariantFloat.New();
                    break;
                case VariantType.String:
                    _Variant = VariantString.New();
                    break;
                case VariantType.PersistID:
                    _Variant = VariantPersistID.New();
                    break;
                case VariantType.Bytes:
                    _Variant = VariantBytes.New();
                    break;
                case VariantType.Int2:
                    _Variant = VariantInt2.New();
                    break;
                case VariantType.Int3:
                   _Variant = VariantInt3.New();
                    break;
                default:
                    break;
            }
        }

        private bool Check(VariantType type)
        {
            return _Variant == null ? false : _Variant.Type == type;
        }
        
        public bool GetBool()
        {
            return _Variant as VariantBoolean;
        }

        public byte GetByte()
        {
            return _Variant as VariantByte;
        }

        public int GetInt()
        {
            return _Variant as VariantInteger;
        }

        public long GetLong()
        {
            return _Variant as VariantLong;
        }

        public float GetFloat()
        {
            return _Variant as VariantFloat;
        }

        public string GetString()
        {
            return _Variant as VariantString;
        }

        public PersistID GetPid()
        {
            return _Variant as VariantPersistID;
        }

        public Bytes GetBytes()
        {
            return _Variant as VariantBytes;
        }

        public Int2 GetInt2()
        {
            return _Variant as VariantInt2;
        }

        public Int3 GetInt3()
        {
            return _Variant as VariantInt3;
        }

        public void SetBool(bool value)
        {
            if (!Check(VariantType.Bool))
            {
                return;
            }

            (_Variant as VariantBoolean).Value = value;
        }

        public void SetByte(byte value)
        {
            if (!Check(VariantType.Byte))
            {
                return;
            }

            (_Variant as VariantByte).Value = value;
        }
        
        public void SetInt(int value)
        {
            if (!Check(VariantType.Int))
            {
                return;
            }

            (_Variant as VariantInteger).Value = value;
        }

        public void SetLong(long value)
        {
            if (!Check(VariantType.Long))
            {
                return;
            }

            (_Variant as VariantLong).Value = value;
        }

        public void SetFloat(float value)
        {
            if (!Check(VariantType.Float))
            {
                return;
            }

            (_Variant as VariantFloat).Value = value;
        }

        public void SetString(string value)
        {
            if (!Check(VariantType.String))
            {
                return;
            }

            (_Variant as VariantString).Value = value;
        }

        public void SetPid(PersistID value)
        {
            if (!Check(VariantType.PersistID))
            {
                return;
            }

            (_Variant as VariantPersistID).Value = value;
        }

        public void SetBytes(Bytes value)
        {
            if (!Check(VariantType.Bytes))
            {
                return;
            }

            (_Variant as VariantBytes).Value = value;
        }

        public void SetInt2(Int2 value)
        {
            if (!Check(VariantType.Int2))
            {
                return;
            }

            (_Variant as VariantInt2).Value = value;
        }

        public void SetInt3(Int3 value)
        {
            if (!Check(VariantType.Int3))
            {
                return;
            }

            (_Variant as VariantInt3).Value = value;
        }
    }
}