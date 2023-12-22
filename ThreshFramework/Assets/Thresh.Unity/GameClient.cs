using System;
using System.Collections;
using System.Collections.Generic;
using Thresh.Unity.Asset;
using Thresh.Unity.Global;
using Thresh.Unity.Localization;
using Thresh.Unity.Utility;
using UnityEngine;

namespace Thresh.Unity
{
    public delegate void GameClientEvent();

    public class GameClient : SingletonBehaviour<GameClient>
    {
        public GameClientEvent Enter;
        public GameClientEvent Exit;
        public GameClientEvent Esc;
        public GameClientEvent Pause;
        public GameClientEvent Continue;
        public GameClientEvent OnNet;
        public GameClientEvent OffNet;
        public GameClientEvent ChangeLanguage;

        public override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            LogAssert.Level = LogLevel.Trace;
#else
            LogAssert.Level = LogLevel.Error;
#endif

            Application.runInBackground = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _EngineDic = new Dictionary<string, IEngine>();
        }

        private void Start()
        {
            StartCoroutine(InitEngines());
        }

        private Dictionary<string, IEngine> _EngineDic;

        void RegisterEngine(string name, IEngine engine)
        {
            if (_EngineDic.ContainsKey(name))
            {
                LogAssert.Util.Warn("register [{0}_engine] failed",name);
                return;
            }
            
            _EngineDic.Add(name,engine);
            LogAssert.Util.Debug("register [{0}_engine] success.",name);
        }
        
        private IEnumerator InitEngines()
        {
            while (!AssetEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("asset",AssetEngine.Instance);

            while (!L10NEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("l10n",AssetEngine.Instance);

            while (!KernelEngine)
            {
                
            }
        }

    }
}