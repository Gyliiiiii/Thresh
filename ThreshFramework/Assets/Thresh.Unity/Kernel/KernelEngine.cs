using System.Collections;
using Thresh.Core.Config;
using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;
using Thresh.Unity.Databind;
using Thresh.Unity.Global;
using UnityEngine;

namespace Thresh.Unity.Kernel
{
    public class KernelEngine : SingletonEngine<KernelEngine>,IEngine
    {
        public CKernel Kernel { get; private set; }
        
        public PersistID MainRole { get; set; }
        
        public Definition Define { get; private set; }

        private void HandleContainerData(IKernel kernel, PersistID self, VariantList args)
        {
            string container_name = args.SubtractString();
            
            DatabindEngine.Instance.CallContainer(self,container_name,args);
        }
        
        private void HandleRecordData(IKernel kernel, PersistID self, VariantList args)
        {
            string record_name = args.SubtractString();

            DatabindEngine.Instance.CallRecord(self, record_name, args);
        }

        private void HandlePropertyData(IKernel kernel, PersistID self, VariantList args)
        {
            string property_name = args.StringAt(0);

            DatabindEngine.Instance.CallProperty(self, property_name, args);
        }
        
        protected override IEnumerator Startup()
        {
            Define = Definition.Load(AssetPath.GameConfigPath);

            yield return new WaitForSeconds(1);

            Kernel = new CKernel(Define);
            
            Kernel.RegisterDataTypeCallback(DataType.Property,HandlePropertyData);
            Kernel.RegisterDataTypeCallback(DataType.Record, HandleRecordData);
            Kernel.RegisterDataTypeCallback(DataType.Container,HandleContainerData);

            yield return new WaitForFixedUpdate();
        }
        
        protected override IEnumerator Shutdown()
        {
            yield return new WaitForFixedUpdate();
        }

        
    }
}