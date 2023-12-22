using System;
using Thresh.Core.Config;
using Thresh.Core.Data;
using Thresh.Core.Event;
using Thresh.Core.Interface;
using Thresh.Core.Kernel;
using Thresh.Core.Variant;

namespace Thresh.Unity.Kernel
{
    public partial class CKernel : IKernel
    {
        private EntityManager          _EntityManager                    = null;
        private DataCallbackManager    _DataCallbackManager              = null;
        private ModuleManager          _ModuleManager                    = null;
        private TimerManager           _TimerManager                     = null;
        private EventManager<int>      _CommandMsgCallbackManager        = null;
        private EventManager<int>      _CustomMsgCallbackManager         = null;
        private EventManager<DataType> _DataTypeCallbackManager          = null;
        private ILog                   _Logger                           = null;


        public CKernel(Definition define)
        {
            _EntityManager = new EntityManager(this);
            _DataCallbackManager = new DataCallbackManager(this);
            _ModuleManager = new ModuleManager(this);
            _TimerManager = new TimerManager(this);
            
        }
        public EntityDef GetEntityDef(string type)
        {
            throw new NotImplementedException();
        }

        public void CreateModule<T>() where T : IModule
        {
            throw new NotImplementedException();
        }

        public void RegisterEntityCallback(string entity_type, EntityEvent event_enum, EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterPropertyCallback(string entity_type, string property_name, PropertyEvent event_enum,
            EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterRecordCallback(string entity_type, string record_name, RecordEvent event_enum, EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterContainerCallback(string entity_type, string container_name, ContainerEvent event_enum,
            EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterDataTypeCallback(DataType data_type, EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public string GetType(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public PersistID CreateEntity(string define, PersistID root, VariantList args = null)
        {
            throw new NotImplementedException();
        }

        public PersistID CreateEntity(PersistID pid, string define, VariantList args = null)
        {
            throw new NotImplementedException();
        }

        public void DestroyEntity(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public VariantList ToProto(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public bool FindProperty(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyBool(PersistID pid, string property_name, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyByte(PersistID pid, string property_name, byte value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyInt(PersistID pid, string property_name, int value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyLong(PersistID pid, string property_name, long value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyFloat(PersistID pid, string property_name, float value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyString(PersistID pid, string property_name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyPid(PersistID pid, string property_name, PersistID value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyBytes(PersistID pid, string property_name, Bytes value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyInt2(PersistID pid, string property_name, Int2 value)
        {
            throw new NotImplementedException();
        }

        public void SetPropertyInt3(PersistID pid, string property_name, Int3 value)
        {
            throw new NotImplementedException();
        }

        public bool GetPropertyBool(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public byte GetPropertyByte(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public int GetPropertyInt(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public long GetPropertyLong(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public float GetPropertyFloat(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyString(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public PersistID GetPropertyPid(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public Bytes GetPropertyBytes(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public Int2 GetPropertyInt2(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public Int3 GetPropertyInt3(PersistID pid, string property_name)
        {
            throw new NotImplementedException();
        }

        public bool FindRecord(PersistID pid, string record_name)
        {
            throw new NotImplementedException();
        }

        public void ClearRecord(PersistID pid, string record_name)
        {
            throw new NotImplementedException();
        }

        public int AddRecordRow(PersistID pid, string record_name, VariantList value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordRow(PersistID pid, string record_name, int row, VariantList value)
        {
            throw new NotImplementedException();
        }

        public VariantList GetRecordRow(PersistID pid, string record_name, int row)
        {
            throw new NotImplementedException();
        }

        public VariantList GetRecordRows(PersistID pid, string record_name)
        {
            throw new NotImplementedException();
        }

        public void DelRecordRow(PersistID pid, string record_name, int row)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowBool(PersistID pid, string record_name, int col, bool value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowByte(PersistID pid, string record_name, int col, byte value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowInt(PersistID pid, string record_name, int col, int value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowFloat(PersistID pid, string record_name, int col, float value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowLong(PersistID pid, string record_name, int col, long value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowString(PersistID pid, string record_name, int col, string value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowPid(PersistID pid, string record_name, int col, PersistID value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowBytes(PersistID pid, string record_name, int col, Bytes value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowInt2(PersistID pid, string record_name, int col, Int2 value)
        {
            throw new NotImplementedException();
        }

        public int FindRecordRowInt3(PersistID pid, string record_name, int col, Int3 value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordBool(PersistID pid, string record_name, int row, int col, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordByte(PersistID pid, string record_name, int row, int col, byte value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordInt(PersistID pid, string record_name, int row, int col, int value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordLong(PersistID pid, string record_name, int row, int col, long value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordFloat(PersistID pid, string record_name, int row, int col, float value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordString(PersistID pid, string record_name, int row, int col, string value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordPid(PersistID pid, string record_name, int row, int col, PersistID value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordBytes(PersistID pid, string record_name, int row, int col, Bytes value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordInt2(PersistID pid, string record_name, int row, int col, Int2 value)
        {
            throw new NotImplementedException();
        }

        public void SetRecordInt3(PersistID pid, string record_name, int row, int col, Int3 value)
        {
            throw new NotImplementedException();
        }

        public bool GetRecordBool(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public byte GetRecordByte(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public int GetRecordInt(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public long GetRecordLong(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public float GetRecordFloat(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public string GetRecordString(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public PersistID GetRecordPid(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public Bytes GetRecordBytes(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public Int2 GetRecordInt2(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public Int3 GetRecordInt3(PersistID pid, string record_name, int row, int col)
        {
            throw new NotImplementedException();
        }

        public bool FindTemporary(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryBool(PersistID pid, string temporary_name, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryByte(PersistID pid, string temporary_name, byte value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryInt(PersistID pid, string temporary_name, int value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryLong(PersistID pid, string temporary_name, long value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryFloat(PersistID pid, string temporary_name, float value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryString(PersistID pid, string temporary_name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryPid(PersistID pid, string temporary_name, PersistID value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryBytes(PersistID pid, string temporary_name, Bytes value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryInt2(PersistID pid, string temporary_name, Int2 value)
        {
            throw new NotImplementedException();
        }

        public void SetTemporaryInt3(PersistID pid, string temporary_name, Int3 value)
        {
            throw new NotImplementedException();
        }

        public bool GetTemporaryBool(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public byte GetTemporaryByte(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public int GetTemporaryInt(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public long GetTemporaryLong(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public float GetTemporaryFloat(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public string GetTemporaryString(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public PersistID GetTemporaryPid(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public Bytes GetTemporaryBytes(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public Int2 GetTemporaryInt2(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public Int3 GetTemporaryInt3(PersistID pid, string temporary_name)
        {
            throw new NotImplementedException();
        }

        public void SetAttributeInt(PersistID pid, string attribute_name, int value)
        {
            throw new NotImplementedException();
        }

        public void SetAttributeString(PersistID pid, string attribute_name, string value)
        {
            throw new NotImplementedException();
        }

        public int GetAttributeInt(PersistID pid, string attribute_name)
        {
            throw new NotImplementedException();
        }

        public string GetAttributeString(PersistID pid, string attribute_name)
        {
            throw new NotImplementedException();
        }

        public bool FindContainer(PersistID pid, string container_name)
        {
            throw new NotImplementedException();
        }

        public void AddChild(PersistID pid, string container_name, PersistID child)
        {
            throw new NotImplementedException();
        }

        public void AddChild(PersistID pid, string container_name, int slot, PersistID child)
        {
            throw new NotImplementedException();
        }

        public void DelChild(PersistID pid, string container_name, PersistID child)
        {
            throw new NotImplementedException();
        }

        public void DelChild(PersistID pid, string container_name, int slot)
        {
            throw new NotImplementedException();
        }

        public PersistID GetChild(PersistID pid, string container_name, int slot)
        {
            throw new NotImplementedException();
        }

        public int GetChildCnt(PersistID pid, string container_name)
        {
            throw new NotImplementedException();
        }

        public int GetSlot(PersistID pid, string container_name, PersistID child)
        {
            throw new NotImplementedException();
        }

        public int GetCapacity(PersistID pid, string container_name)
        {
            throw new NotImplementedException();
        }

        public VariantList GetChildren(PersistID pid, string container_name)
        {
            throw new NotImplementedException();
        }

        public bool FindChild(PersistID pid, string container_name, PersistID child)
        {
            throw new NotImplementedException();
        }

        public PersistID GetRoot(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public PersistID GetParent(PersistID pid)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommandCallback(int command_msg, EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomCallback(int custom_msg, EventCallback handler)
        {
            throw new NotImplementedException();
        }

        public void AddCountdown(PersistID pid, string countdown_name, long over_millseconds, TimerCallback countdown)
        {
            throw new NotImplementedException();
        }

        public void DelCountdown(PersistID pid, string countdown_name)
        {
            throw new NotImplementedException();
        }

        public void AddHeartbeat(PersistID pid, string heartbeat_name, long gap_millseconds, int count, TimerCallback heartbeat)
        {
            throw new NotImplementedException();
        }

        public void DelHeartbeat(PersistID pid, string heartbeat_name)
        {
            throw new NotImplementedException();
        }
    }
}