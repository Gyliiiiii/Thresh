using Thresh.Core.Data;

namespace Thresh.Core.Interface
{
    public interface ITemporary
    {
        string Name { get; }

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

        void SetBool(bool value);
        void SetByte(byte value);
        void SetInt(int value);
        void SetLong(long value);
        void SetFloat(float value);
        void SetString(string value);
        void SetPid(PersistID value);
        void SetBytes(Bytes value);
        void SetInt2(Int2 value);
        void SetInt3(Int3 value);
    }
}