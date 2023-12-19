using System;
using System.Collections.Generic;
using System.Text;
using Thresh.Core.Data;
using Thresh.Core.Interface;

namespace Thresh.Core.Kernel
{
    public class ModuleManager
    {
        public ModuleManager(IKernel kernel)
        {
            _Kernel = kernel;
            _ModuleDic = new Dictionary<string, IModule>();
        }

        private IKernel _Kernel;
        private Dictionary<string, IModule> _ModuleDic;

        public IModule GetModule(string name)
        {
            IModule found = null;

            _ModuleDic.TryGetValue(name, out found);

            return found;
        }

        public void CreateModule<T>() where T : IModule
        {
            try
            {
                Type type = typeof(T);
                string name = type.ToString();
                IModule module = Activator.CreateInstance(type) as IModule;

                if (module == null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0} is exist.", name);
                    throw new ModuleException(sb.ToString());
                }
                else if (_ModuleDic.ContainsKey(type.ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0} is exist.", name);
                    throw new ModuleException(sb.ToString());
                }
                else
                {
                    _ModuleDic.Add(name,module);
                    module.Create(_Kernel);
                    _Kernel.Info("CreateModule {0} Success",name);
                }
            }
            catch (Exception ex)
            {
                throw new ModuleException("Error Module Because", ex);
            }
        }
    }
}