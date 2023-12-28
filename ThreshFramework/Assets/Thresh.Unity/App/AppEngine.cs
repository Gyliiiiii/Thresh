using System;
using System.Collections;
using System.Collections.Generic;
using Thresh.Core.Config.INI;
using Thresh.Core.Interface;
using Thresh.Unity.Global;
using UnityEngine;

namespace Thresh.Unity.App
{
    public abstract class AppBase
    {
        protected ILog Logger;

        public abstract void OnAwake(Configuration app_ini);

        public abstract void OnOpen();

        public abstract void OnClose();
        
        public virtual void OnLoop(){ }

        public AppBase()
        {
            Logger = LogAssert.GetLog(GetType().Name);
        }
    }

    public class AppEngine : SingletonEngine<AppEngine>, IEngine
    {
        private Configuration _Ini;

        private Dictionary<string, AppBase> _AppDic = null;
        
        public DispatchApp Dispatch { get; private set; }
        public UserinfoApp Userinfo { get; private set; }
        public override void Awake()
        {
            base.Awake();

            _AppDic = new Dictionary<string, AppBase>();
        }

        APP CreateApp<APP>() where APP : AppBase
        {
            Type type = typeof(APP);
            if (_AppDic.ContainsKey(type.Name))
            {
                LogAssert.Util.Warn("{0} App is exist",type.Name);
                return null;
            }

            APP app = Activator.CreateInstance<APP>();
            app.OnAwake(_Ini);

            return app;
        }

        protected override void Loop()
        {
            Dispatch.OnLoop();
            
            Userinfo.OnLoop();
            
        }
        
        protected override IEnumerator Shutdown()
        {
            Dispatch.OnClose();
            yield return new WaitForFixedUpdate();
            
            Userinfo.OnClose();
            yield return new WaitForFixedUpdate();
        }

        protected override IEnumerator Startup()
        {
           _Ini = Configuration.LoadFromFile(AssetPath.AppIni);

           yield return new WaitForFixedUpdate();

           Dispatch = CreateApp<DispatchApp>();
           Userinfo = CreateApp<UserinfoApp>();
           yield return new WaitForFixedUpdate();
           
           Dispatch.OnOpen();
           Userinfo.OnOpen();
        }

        public bool ConnectionSuccess = false;

        public const int GuestLoginType = 0;
        public const int AccountLoginType = 1;
        public const int AccountRegisterType = 2;
        public int LoginType = 0;
        public void Login()
        {
            if (LoginType == GuestLoginType)
            {
                if (string.IsNullOrEmpty(Userinfo.Token))
                {
                    Logger.Debug("GuestRegister: {0}\r\n",Userinfo.Uuid);
                }
                else
                {
                    Logger.Debug("GuestLogin: {0}\r\n{1}",Userinfo.Uuid,Userinfo.Token);
                }
            }
            else if(LoginType == AccountRegisterType)
            {
                Logger.Debug("UserRegister: {0}\r\n",Userinfo.Uuid);
            }
            else
            {
                Logger.Debug("UserLogin: {0}\r\n{1}",Userinfo.GetAccount(),Userinfo.GetPassword());
            }   
        }
    }
}