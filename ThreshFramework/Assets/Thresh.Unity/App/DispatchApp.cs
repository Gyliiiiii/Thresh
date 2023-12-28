using System;
using System.Collections.Generic;
using Thresh.Core.Config.INI;
using Thresh.Core.Data;
using Thresh.Core.Variant;
using Thresh.Network;
using Thresh.Unity.Databind;
using Thresh.Unity.Global;
using Thresh.Unity.Kernel;
using Thresh.Unity.Native;

namespace Thresh.Unity.App
{
    public class DispatchApp : AppBase
    {
        private Dictionary<int, Action<VariantList>> _MessageHandlers = null;

        private CKernel Kernel
        {
            get { return KernelEngine.Instance.Kernel; }
        }

        public override void OnAwake(Configuration app_ini)
        {
            _MessageHandlers = new Dictionary<int, Action<VariantList>>();
        }

        public override void OnOpen()
        {
            RegisterMessageHandler(SystemOpcode.SERVER.SHAKE_HANDS, OnRecvMsg_ShakeHands);
        }

        private void OnRecvMsg_ShakeHands(VariantList recv_msg)
        {
            string session_key = recv_msg.SubtractString();

            if (!NativeEngine.Instance.SDKLoginSuccess)
            {
                AppEngine.Instance.Userinfo.SessionKey = session_key;
            }
            else
            {
                PersistID role_id = KernelEngine.Instance.MainRole;
                string _sessionkey = AppEngine.Instance.Userinfo.SessionKey;
                LogAssert.Util.Trace("session_key:{0}" + _sessionkey);
            }

            AppEngine.Instance.Login();
        }

        public override void OnClose()
        {
        }

        public void DispatchMessage(int opcode, VariantList msg)
        {
            Action<VariantList> handler;

            if (_MessageHandlers.TryGetValue(opcode,out handler))
            {
                handler(msg);
            }

            DatabindEngine.Instance.CallSystemMsg(opcode, msg);
        }

        private void RegisterMessageHandler(int opcode, Action<VariantList> handle)
        {
            if (_MessageHandlers.ContainsKey(opcode))
            {
                Logger.Warn("RegisterMessageHandler opcode={0} is exist.", opcode);
                return;
            }

            _MessageHandlers.Add(opcode, handle);
        }
    }
}