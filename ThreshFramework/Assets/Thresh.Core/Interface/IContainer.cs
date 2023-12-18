namespace Thresh.Core.Interface
{
    public interface IContainer
    {
        string Name { get; }
        int Capacity { get; }
        int FreeSlot { get; }
        void Clear();

    }
}