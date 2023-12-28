using System.Collections;
using Thresh.Core.Config.INI;
using Thresh.Unity.Global;
using UnityEngine;

namespace Thresh.Unity.Native
{
    public class NativeEngine : SingletonEngine<NativeEngine>, IEngine
    {
        private Configuration _Ini;
        
        public bool SDKLoginSuccess { get; set; }
        
        protected override IEnumerator Shutdown()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator Startup()
        {
            _Ini = Configuration.LoadFromFile(AssetPath.SDKIni);

            yield return new WaitForFixedUpdate();
        }
    }
}