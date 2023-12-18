
using System.Collections.Generic;
using Thresh.Core.Data;

namespace Thresh.Core.Kernel
{
    public class EntityManager
    {
        private Dictionary<PersistID, Entity> _EntityDic;

        private Entity _Entity;

        private IKernel _Kernel;
    }
}
