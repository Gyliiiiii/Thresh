using Thresh.Core.Variant;

namespace Thresh.Core.Interface
{
    public interface IRecord
    {
        string Name { get; }

        VariantList ColTypes { get; }

        int MaxRows { get; }

        void Clear();
    }
}