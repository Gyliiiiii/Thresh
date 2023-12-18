using Thresh.Core.Data;
using Thresh.Core.Interface;

namespace Thresh.Core.Kernel
{
    public delegate void TimerCallback(IKernel kernel, PersistID pid, string name, long now, int count);
    public class TimerManager
    {
        
    }
}