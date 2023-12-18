using Thresh.Core.Data;
using Thresh.Core.Variant;

namespace Thresh.Core.Interface
{
    public interface IContainer
    {
        string Name { get; }
        int Capacity { get; }
        int FreeSlot { get; }
        void Clear();
        
        PersistID[] GetChildren();
        PersistID GetChild(int slot);
        int GetSlot(PersistID child);

        bool TryAddChild(int slot, PersistID pid, out VariantList result);
        bool TryAddChild(PersistID pid, out VariantList result);
        bool TryDelChild(PersistID pid, out VariantList result);
        bool TryDelChild(int slot, out VariantList result);

    }
}