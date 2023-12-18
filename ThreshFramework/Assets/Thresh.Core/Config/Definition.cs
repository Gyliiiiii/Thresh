using System;
using System.Collections.Generic;
using System.Text;
using Thresh.Core.Data;
using Thresh.Core.Variant;
using Unity.VisualScripting;

namespace Thresh.Core.Config
{
    public class Definition
    {
        private Dictionary<string, EntityDef> _DefinitionDic = null;
    }

    public class PropertyDef
    {
        public string Name { get; set; }
        
        public VariantMap Define { get; set; }

        public PropertyDef(string name, VariantMap define)
        {
            Name = name;
            Define = define;
        }
    }

    public class RecordDef
    {
        public string Name { get; set; }
        
        public VariantMap Define { get; set; }
        
        public VariantList ColTypes { get; set; }
        
        public VariantList ColNames { get; set; }
        
        public VariantList ColDescs { get; set; }

        public int ColOf(string col_name)
        {
            for (int col = 0; col < ColNames.Count; col++)
            {
                if (col_name.Equals(ColNames.StringAt(col)))
                {
                    return col;
                }
            }

            return Constant.INVALID_COL;
        }

        public RecordDef(string name, VariantMap define, VariantList col_types, VariantList col_names,
            VariantList col_descs)
        {
            Name = name;
            Define = define;
            ColTypes = col_types;
            ColNames = col_names;
            ColDescs = col_descs;
        }
    }

    public class ContainerDef
    {
        public string Name { get; set; }
        
        public VariantMap Define { get; set; }

        public ContainerDef(string name, VariantMap define)
        {
            Name = name;
            Define = define;
        }
    }
    
    
    public class EntityDef
    {
        private Dictionary<string, PropertyDef> _PropertyDefinitions;

        private Dictionary<string, RecordDef> _RecordDefinitions;
        
        private Dictionary<string, ContainerDef> _ContainerDefinitions;

        private string _Name;
        public bool isFramework = false;
        private string _Node = Constant.DEFAULT_NODE;
        
        public EntityDef Parent { get; set; }

        private VariantList _InheritList;

        public VariantList InheritList
        {
            get
            {
                if (_InheritList.Count > 0)
                {
                    return _InheritList;
                }

                FindParent(this, ref _InheritList);

                return _InheritList;
            }
        }

        public void AddChild(string child_type)
        {
            if (Parent != null)
            {
                AddChild(child_type);
            }

            if (!_Children.Contains(child_type))
            {
                _Children.Add(child_type);
            }
        }

        private HashSet<string> _Children;

        private VariantList _ChildList;

        public VariantList ChildList
        {
            get
            {
                if (_ChildList.Count>0)
                {
                    return _ChildList;
                }

                foreach (var child in _Children)
                {
                    _ChildList.Append(child);
                }

                return _ChildList;
            }
        }

        private VariantList _ChildListAndSelf;

        public VariantList ChildListAndSelf
        {
            get
            {
                if (_ChildListAndSelf.Count > 0)
                {
                    return _ChildListAndSelf;
                }

                _ChildListAndSelf.Append(Name);
                foreach (var child in _Children)
                {
                    _ChildListAndSelf.Append(child);
                }

                return _ChildListAndSelf;
            }
        }

        private static void FindParent(EntityDef entity, ref VariantList list)
        {
            if (entity.Parent != null)
            {
                list.Append(entity.Parent.Name);
                FindParent(entity.Parent,ref list);
            }
        }
        
        public string Name { get { return _Name; } }
        
        public string Node
        {
            get { return _Node; }

            set { _Node = value; }
        }

        public EntityDef(string name)
        {
            _Name = name;
            _PropertyDefinitions = new Dictionary<string, PropertyDef>();
            _RecordDefinitions = new Dictionary<string, RecordDef>();
            _ContainerDefinitions = new Dictionary<string, ContainerDef>();
            _InheritList = VariantList.New();
            _ChildList = VariantList.New();
            _Children = new HashSet<string>();
            _ChildListAndSelf = VariantList.New();
        }

        public void AddProperty(string property_name, VariantMap define)
        {
            try
            {
                if (_PropertyDefinitions.ContainsKey(property_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("AddProerty Fail Name {0} is exit", property_name);
                }
                else
                {
                    PropertyDef property = new PropertyDef(property_name, define);
                    _PropertyDefinitions.Add(property_name, property);
                }
            }
            catch (Exception ex)
            {
                throw new DefinitionException("DefinitionException because", ex);
            }
        }

        public void AddRecord(string record_name, VariantMap define, VariantList col_types, VariantList col_names,
            VariantList col_descs)
        {
            try
            {
                if (_RecordDefinitions.ContainsKey(record_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("AddRecord Fail Name {0} is exist", record_name);
                }
                else
                {
                    RecordDef record = new RecordDef(record_name, define, col_types, col_names, col_descs);
                    _RecordDefinitions.Add(record_name, record);
                }
            }
            catch (Exception ex)
            {
                throw new DefinitionException("DefinitionExpection because ", ex);
            }
        }
        
        public void AddContainer(string container_name, VariantMap define)
        {
            try
            {
                if (_ContainerDefinitions.ContainsKey(container_name))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("AddContainer Fail Name {0} is exist", container_name);
                }
                else
                {
                    ContainerDef record = new ContainerDef(container_name, define);
                    _ContainerDefinitions.Add(container_name, record);
                }
            }
            catch (Exception ex)
            {
                throw new DefinitionException("DefinitionExpection because ", ex);
            }
        }

        private PropertyDef[] _AllProperties;

        public PropertyDef[] GetAllProperties()
        {
            if (_AllProperties != null) return _AllProperties;

            List<PropertyDef> list = new List<PropertyDef>();
            GetParentProperties(this, ref list);

            _AllProperties = list.ToArray();
            return _AllProperties;
        }
        
        private static void GetParentProperties(EntityDef entity, ref List<PropertyDef> list)
        {
            if (entity == null)
            {
                return;
            }

            PropertyDef[] self_properties = entity.GetProperties();
            if (self_properties == null)
            {
                return;
            }

            list.AddRange(self_properties);

            GetParentProperties(entity.Parent, ref list);
        }
        
        private RecordDef[] _AllRecords;
        public RecordDef[] GetAllRecords()
        {
            if (_AllRecords != null) return _AllRecords;

            List<RecordDef> list = new List<RecordDef>();
            GetParentRecords(this, ref list);

            _AllRecords = list.ToArray();
            return _AllRecords;
        }

        private static void GetParentRecords(EntityDef entity, ref List<RecordDef> list)
        {
            if (entity == null)
            {
                return;
            }

            RecordDef[] self_records = entity.GetRecords();
            if (self_records == null)
            {
                return;
            }

            list.AddRange(self_records);

            GetParentRecords(entity.Parent, ref list);
        }

        private ContainerDef[] _AllContainers;
        public ContainerDef[] GetAllContainers()
        {
            if (_AllContainers != null) return _AllContainers;

            List<ContainerDef> list = new List<ContainerDef>();
            GetParentContainers(this, ref list);

            _AllContainers = list.ToArray();
            return _AllContainers;
        }

        private static void GetParentContainers(EntityDef entity, ref List<ContainerDef> list)
        {
            if (entity == null)
            {
                return;
            }

            ContainerDef[] self_containers = entity.GetContainers();
            if (self_containers == null)
            {
                return;
            }

            list.AddRange(self_containers);

            GetParentContainers(entity.Parent, ref list);
        }
        
        private PropertyDef[] _Properties;
        public PropertyDef[] GetProperties()
        {
            if (_Properties == null)
            {
                _Properties = new PropertyDef[_PropertyDefinitions.Count];
                _PropertyDefinitions.Values.CopyTo(_Properties, 0);
            }

            return _Properties;
        }
        
        private RecordDef[] _Records;
        public RecordDef[] GetRecords()
        {
            if (_Records == null)
            {
                _Records = new RecordDef[_RecordDefinitions.Count];
                _RecordDefinitions.Values.CopyTo(_Records, 0);
            }
            return _Records;
        }

        private ContainerDef[] _Containers;
        public ContainerDef[] GetContainers()
        {
            if (_Containers == null)
            {
                _Containers = new ContainerDef[_ContainerDefinitions.Count];
                _ContainerDefinitions.Values.CopyTo(_Containers, 0);
            }
            return _Containers;
        }

        public PropertyDef GetProperty(string property_name)
        {
            PropertyDef[] properties = GetAllProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name.Equals(property_name))
                {
                    return properties[i];
                }
            }

            return null;
        }

        public RecordDef GetRecord(string record_name)
        {
            RecordDef[] records = GetAllRecords();
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].Name.Equals(record_name))
                {
                    return records[i];
                }
            }

            return null;
        }

        public ContainerDef GetContainer(string container_name)
        {
            ContainerDef[] containers = GetAllContainers();
            for (int i = 0; i < containers.Length; i++)
            {
                if (containers[i].Name.Equals(container_name))
                {
                    return containers[i];
                }
            }

            return null;
        }
    }
}