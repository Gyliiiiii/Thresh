using System.Collections;
using Thresh.Core.Data;
using Thresh.Core.Variant;
using UnityEngine;

namespace Thresh.Unity.Databind
{
    public class DatabindEngine : SingletonEngine<DatabindEngine>,IEngine
    {
        private DataBinder _DataBinder;

        private MsgBinder _CustomMsgBinder;

        private MsgBinder _SystemMsgBinder;

        public override void Awake()
        {
            base.Awake();

            _DataBinder = new DataBinder();
            _CustomMsgBinder = new MsgBinder();
            _SystemMsgBinder = new MsgBinder();
        }
        
        protected override IEnumerator Startup()
        {
            yield return new WaitForFixedUpdate();
        }
        
        protected override IEnumerator Shutdown()
        {
            yield return new WaitForFixedUpdate();
        }

        public void RemoveSystemMsg(int system_id, MsgCallback callback)
        {
            _SystemMsgBinder.Cancel(system_id,callback);
        }

        public void BindSystemMsg(int system_id, MsgCallback callback)
        {
            _SystemMsgBinder.Register(system_id,callback);
        }

        public void CallSystemMsg(int system_id, VariantList args)
        {
            _SystemMsgBinder.Callback(system_id,args);
        }

        public void BindCustomMsg(int custom_id, MsgCallback callback)
        {
            _CustomMsgBinder.Register(custom_id,callback);
        }

        public void RemoveCustomMsg(int custom_id, MsgCallback callback)
        {
            _CustomMsgBinder.Cancel(custom_id,callback);
        }

        public void CallCustomMsg(int custom_id, VariantList args)
        {
            _CustomMsgBinder.Callback(custom_id,args);
        }
        
           public void BindProperty(PersistID pid, string property_name, DataCallback calllback)
        {
            _DataBinder.Register(DataType.Property, pid, property_name, calllback);
        }
        public void RemoveBindProperty(PersistID pid, string property_name, DataCallback calllback)
        {
            _DataBinder.UnRegister(DataType.Property, pid, property_name, calllback);
        }
        public void BindPropertys(PersistID pid, DataCallback calllback)
        {
            _DataBinder.Register(DataType.Property, pid, calllback);
        }
        public void RemoveBindPropertys(PersistID pid, DataCallback calllback)
        {
            _DataBinder.UnRegister(DataType.Property, pid, calllback);
        }

        public void BindRecord(PersistID pid, string record_name, DataCallback calllback)
        {
            _DataBinder.Register(DataType.Record, pid, record_name, calllback);
        }
        public void RemoveBindRecord(PersistID pid, string record_name, DataCallback calllback)
        {
            _DataBinder.UnRegister(DataType.Record, pid, record_name, calllback);
        }
        public void BindContainer(PersistID pid, string container_name, DataCallback calllback)
        {
            _DataBinder.Register(DataType.Container, pid, container_name, calllback);
        }
        public void RemoveBindContainer(PersistID pid, string container_name, DataCallback handler)
        {
            _DataBinder.UnRegister(DataType.Container, pid, container_name, handler);
        }

        public void CallProperty(PersistID pid, string property_name, VariantList args)
        {
            _DataBinder.Callback(DataType.Property, pid, property_name, args);
        }

        public void CallRecord(PersistID pid, string record_name, VariantList args)
        {
            _DataBinder.Callback(DataType.Record, pid, record_name, args);
        }

        public void CallContainer(PersistID pid, string container_name, VariantList args)
        {
            _DataBinder.Callback(DataType.Container, pid, container_name, args);
        }
    }
}