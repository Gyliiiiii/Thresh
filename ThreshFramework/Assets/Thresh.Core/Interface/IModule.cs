namespace Thresh.Core.Interface
{
    public interface IModule
    {
        void Create(IKernel kernel);

        void Destroy(IKernel kernel);
    }
}