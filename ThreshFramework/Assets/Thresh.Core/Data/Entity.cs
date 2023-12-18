using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using Thresh.Core.Interface;
using Thresh.Core.Variant;
using Unity.VisualScripting;

namespace Thresh.Core.Data
{
    [ProtoContract]
    public class Entity : IEntity
    {

        #region  ------- -------- ------         Dictionary           ------ -------- -------
        [ProtoMember(Constant.DATA_PROTO_ENTITY_PROPERTIES)]
        private Dictionary<string, IProperty> _PropertyDic = null;
        [ProtoMember(Constant.DATA_PROTO_ENTITY_RECORDS)]
        private Dictionary<string, IRecord> _RecordDic = null;
        [ProtoMember(Constant.DATA_PROTO_ENTITY_CONTAINERS)]
        private Dictionary<string, IContainer> _ContainerDic = null;
        private Dictionary<string, Temporary> _TemporaryDic = null;
        #endregion

        #region ------- -------- ------         Cache           ------ -------- -------
        private IProperty _Property = null;
        private IRecord _Record = null;
        private IContainer _Container = null;
        private Temporary _Temporary = null;
        #endregion

        public Entity()
        {
            _ContainerDic = new Dictionary<string, IContainer>();
            _PropertyDic = new Dictionary<string, IProperty>();
            _RecordDic = new Dictionary<string, IRecord>();
            _TemporaryDic = new Dictionary<string, Temporary>();
            OnEntry = false;
        }

        [ProtoMember(Constant.DATA_PROTO_ENTITY_SELF_PID)]
        private PersistID _Self;

        public PersistID Self
        {
            get
            {
                return _Self;
            }
            set
            {
                _Self = value;
            }
        }

        [ProtoMember(Constant.DATA_PROTO_ENTITY_TYPE)]
        private string _Type;

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        
        public bool OnEntry { get; set; }
        

        public void Clear()
        {
            foreach (Property property in _PropertyDic.Values)
            {
                property.Clear();
            }

            foreach (Record record in _RecordDic.Values)
            {
                record.Clear();
            }
            
            foreach(Container container in _ContainerDic.Values)
            {
                container.Clear();
            }
        }

        #region ------- -------- ------         Load           ------ -------- -------
        public void LoadProperty(string property_name, string property_value)
        {
            IProperty property = GetProperty(property_name);
            if (property == null)
            {
                return;
            }

            VariantList result;
            switch (_Property.Type)
            {
                case VariantType.Bool:
                    property.TrySetBool(bool.Parse(property_value), out result);
                    break;
                case VariantType.Byte:
                    property.TrySetByte(byte.Parse(property_value), out result);
                    break;
                case VariantType.Int:
                    property.TrySetInt(int.Parse(property_value), out result);
                    break;
                case VariantType.Long:
                    property.TrySetLong(long.Parse(property_value), out result);
                    break;
                case VariantType.Float:
                    property.TrySetFloat(float.Parse(property_value), out result);
                    break;
                case VariantType.String:
                    property.TrySetString(property_value, out result);
                    break;
                case VariantType.PersistID:
                    property.TrySetPid(PersistID.Parse(property_value), out result);
                    break;
                case VariantType.Bytes:
                    property.TrySetBytes(Bytes.Parse(property_value), out result);
                    break;
            }
        }
        #endregion

        #region ------- -------- ------         Get           ------ -------- -------

        public IProperty GetProperty(string property_name)
        {
            if (_Property != null && _Property.Name == property_name)
            {
                return _Property;
            }

            _PropertyDic.TryGetValue(property_name, out _Property);
            return _Property;
        }
        
        public IRecord GetRecord(string record_name)
        {
            if (_Record != null && _Record.Name.Equals(record_name))
            {
                return _Record;
            }

            _RecordDic.TryGetValue(record_name, out _Record);

            return _Record;
        }

        public IContainer GetContainer(string container_name)
        {
            if (_Container != null && _Container.Name.Equals(container_name))
            {
                return _Container;
            }

            _ContainerDic.TryGetValue(container_name, out _Container);

            return _Container;
        }

        public ITemporary GetTemporary(string temporary_name)
        {
            if (_Temporary != null && _Temporary.Name.Equals(temporary_name))
            {
                return _Temporary;
            }

            _TemporaryDic.TryGetValue(temporary_name, out _Temporary);

            return _Temporary;
        }
        #endregion

        #region ------- -------- ------         Gets           ------ -------- -------
        public IProperty[] GetProperties()
        {
            Property[] found = new Property[_PropertyDic.Values.Count];
            _PropertyDic.Values.CopyTo(found, 0);

            return found;
        }

        public IRecord[] GetRecords()
        {
            Record[] found = new Record[_RecordDic.Values.Count];
            _RecordDic.Values.CopyTo(found, 0);

            return found;
        }

        public IContainer[] GetContainers()
        {
            Container[] found = new Container[_ContainerDic.Values.Count];
            _ContainerDic.Values.CopyTo(found, 0);

            return found;
        }

        public ITemporary[] GetTemporaries()
        {
            Temporary[] found = new Temporary[_TemporaryDic.Values.Count];
            _TemporaryDic.Values.CopyTo(found, 0);

            return found;
        }
        #endregion

        #region ------- -------- ------         Find           ------ -------- -------
        public bool FindProperty(string property_name)
        {
            return _PropertyDic.ContainsKey(property_name);
        }

        public bool FindRecord(string record_name)
        {
            return _RecordDic.ContainsKey(record_name);
        }

        public bool FindContainer(string container_name)
        {
            return _ContainerDic.ContainsKey(container_name);
        }

        public bool FindTemporary(string temporary_name)
        {
            return _TemporaryDic.ContainsKey(temporary_name);
        }
        #endregion

        #region ------- -------- ------         Create           ------ -------- -------
        public IProperty createProperty(string property_name, VariantType property_type)
        {
            Property property = null;
            try
            {
                if (property_type == VariantType.None)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Property [{0}] Fail Type None", property_name);
                    throw new EntityException(sb.ToString());
                }
                else if (FindProperty(property_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Property Fail [{0}] is exist", property_name);
                    throw new EntityException(sb.ToString());
                }
                else
                {
                    property = new Property(property_name, property_type);
                    _PropertyDic.Add(property_name, property);
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("EntityExpection because ", ex);
            }

            return property;
        }

        public IRecord CreateRecord(string record_name, int rows, VariantList col_types)
        {
            Record record = null;
            try
            {
                if (FindRecord(record_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Record Fail [{0}] is exist", record_name);
                    throw new EntityException(sb.ToString());
                }
                else
                {
                    record = new Record(record_name, rows, col_types as VariantList);
                    _RecordDic.Add(record_name, record);
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("EntityExpection because ", ex);
            }

            return record;
        }

        public IContainer CreateContainer(string container_name, int capacity)
        {
            Container container = null;
            try
            {
                if (FindContainer(container_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Container Fail [{0}] is exist", container_name);
                    throw new EntityException(sb.ToString());
                }
                else
                {
                    container = new Container(container_name, capacity);
                    _ContainerDic.Add(container_name, container);
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("EntityExpection because ", ex);
            }

            return container;
        }

        public ITemporary CreateTemporary(string temporary_name, VariantType temporary_type)
        {
            Temporary temporary = null;
            try
            {
                if (temporary_type == VariantType.None)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Temporary [{0}] Fail Type None", temporary_name);
                    throw new EntityException(sb.ToString());
                }
                else if (FindTemporary(temporary_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Create Temporary Fail [{0}] is exist", temporary_name);
                    throw new EntityException(sb.ToString());
                }
                else
                {
                    temporary = new Temporary(temporary_name, temporary_type);
                    _TemporaryDic.Add(temporary_name, temporary);
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("EntityExpection because ", ex);
            }

            return temporary;
        }
        #endregion
    }
}