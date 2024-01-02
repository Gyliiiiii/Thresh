using Thresh.Core.Data;
using Thresh.Core.Variant;

namespace Thresh.Core.Interface
{
    public interface IRecord
    {
        string Name { get; }

        VariantList ColTypes { get; }

        int MaxRows { get; }

        void Clear();
        
        bool IsFree(int row);

        bool TryAddRow(VariantList row_value, out VariantList result);
        bool TrySetRow(int row, VariantList row_value, out VariantList result);
        bool TryDelRow(int row, out VariantList result);
        VariantList GetRow(int row);
        VariantList GetRows();

        bool TrySetBool(int row, int col, bool value, out VariantList result);
        bool TrySetByte(int row, int col, byte value, out VariantList result);
        bool TrySetInt(int row, int col, int value, out VariantList result);
        bool TrySetLong(int row, int col, long value, out VariantList result);
        bool TrySetFloat(int row, int col, float value, out VariantList result);
        bool TrySetString(int row, int col, string value, out VariantList result);
        bool TrySetPid(int row, int col, PersistID value, out VariantList result);
        bool TrySetBytes(int row, int col, Bytes value, out VariantList result);
        bool TrySetInt2(int row, int col, Int2 value, out VariantList result);
        bool TrySetInt3(int row, int col, Int3 value, out VariantList result);

        bool      GetBool(int row, int col);
        byte      GetByte(int row, int col);
        int       GetInt(int row, int col);
        long      GetLong(int row, int col);
        float     GetFloat(int row, int col);
        string    GetString(int row, int col);
        PersistID GetPid(int row, int col);
        Bytes     GetBytes(int row, int col);
        Int2      GetInt2(int row, int col);
        Int3      GetInt3(int row, int col);

        int FindRowBool(int col, bool value);
        int FindRowByte(int col, byte value);
        int FindRowInt(int col, int value);
        int FindRowLong(int col, long value);
        int FindRowFloat(int col, float value);
        int FindRowString(int col, string value);
        int FindRowPid(int col, PersistID value);
        int FindRowBytes(int col, Bytes value);
        int FindRowInt2(int col, Int2 value);
        int FindRowInt3(int col, Int3 value);
    }
}