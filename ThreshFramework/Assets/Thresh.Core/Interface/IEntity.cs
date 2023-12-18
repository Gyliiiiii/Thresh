using ProtoBuf;
using Thresh.Core.Data;
using Thresh.Core.Variant;

namespace Thresh.Core.Interface
{
    [ProtoContract]
    [ProtoInclude(Constant.DATA_PROTO_ENITITY, typeof(Entity))]
    public interface IEntity
    {
        PersistID Self { get; }
        
        string Type { get; }
        
        bool OnEntry { get; set; }

        bool FindProperty(string property_name);
        bool FindRecord(string record_name);
        bool FindContainer(string container_name);
        bool FindTemporary(string temporary_name);
        
        IProperty GetProperty(string property_name);
        IRecord GetRecord(string record_name);
        IContainer GetContainer(string container_name);
        ITemporary GetTemporary(string temporary_name);
        
        IProperty[] GetProperties();
        IRecord[] GetRecords();
        IContainer[] GetContainers();
        ITemporary[] GetTemporaries();
        
        IProperty createProperty(string property_name, VariantType property_type);
        IRecord CreateRecord(string record_name, int rows, VariantList col_types);
        IContainer CreateContainer(string container_name, int capacity);
        ITemporary CreateTemporary(string temporary_name, VariantType temporary_type);
    }
}