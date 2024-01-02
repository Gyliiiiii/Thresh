using Thresh.Core.Data;
using Thresh.Core.Variant;

namespace Thresh.Core.Interface
{
    public interface IProperty
    {
        string Name { get; }
        VariantType Type { get; }
        void Clear();
        
        bool      GetBool();
        byte      GetByte();
        int       GetInt();
        long      GetLong();
        float     GetFloat();
        string    GetString();
        PersistID GetPid();
        Bytes     GetBytes();
        Int2      GetInt2();
        Int3      GetInt3();

        bool TrySetBool(bool value, out VariantList result);
        bool TrySetByte(byte value, out VariantList result);
        bool TrySetInt(int value, out VariantList result);
        bool TrySetLong(long value, out VariantList result);
        bool TrySetFloat(float value, out VariantList result);
        bool TrySetString(string value, out VariantList result);
        bool TrySetPid(PersistID value, out VariantList result);
        bool TrySetBytes(Bytes value, out VariantList result);
        bool TrySetInt2(Int2 value, out VariantList result);
        bool TrySetInt3(Int3 value, out VariantList result);
    }
}