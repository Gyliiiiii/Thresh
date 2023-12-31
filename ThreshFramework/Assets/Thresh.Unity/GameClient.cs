﻿using System;
using System.Collections;
using System.Collections.Generic;
using Thresh.Unity.App;
using Thresh.Unity.Asset;
using Thresh.Unity.Databind;
using Thresh.Unity.Global;
using Thresh.Unity.Kernel;
using Thresh.Unity.Localization;
using Thresh.Unity.Native;
using Thresh.Unity.Stage;
using Thresh.Unity.UI;
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

            while (!KernelEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("Kernel",KernelEngine.Instance);

            while (!DatabindEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("databind",DatabindEngine.Instance);

            while (!UIEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("ui",UIEngine.Instance);

            while (!StageEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("stage",StageEngine.Instance);

            // while (!NativeEngine.Instance.LoadFinsh)
            // {
            //     yield return null;
            // }
            // RegisterEngine("native",NativeEngine.Instance);

            while (!AppEngine.Instance.LoadFinsh)
            {
                yield return null;
            }
            RegisterEngine("app",AppEngine.Instance);

            foreach (var engine in _EngineDic.Values)
            {
                engine.Running = true;
            }

            if (Enter != null) Enter();
        }

        private void OnApplicationQuit()
        {
            if (Exit != null) Exit();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause && Pause != null) Pause();
            else if (!pause && Continue != null) Continue();
        }

        private bool _ReconnectNet = false;

        private void Update()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                if (OffNet != null && !_ReconnectNet)
                {
                    OffNet();
                }

                _ReconnectNet = true;
            }
            else
            {
                if (OnNet != null && _ReconnectNet)
                {
                    OnNet();
                    _ReconnectNet = false;
                }
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                if (Esc != null) Esc();
            }
        }
    }
}