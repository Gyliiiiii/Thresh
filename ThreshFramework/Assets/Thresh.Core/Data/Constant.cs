namespace Thresh.Core.Data
{
    public class Constant
    {
        public const bool   NULL_BOOLEAN     = false;
        public const byte   NULL_BYTE        = 0;
        public const int    NULL_INTEGER     = 0;
        public const long   NULL_LONG        = 0L;
        public const float  NULL_FLOAT       = 0.0f;
        public const string NULL_STRING      = "";
        
        public const int    INVALID_SLOT     = -1;
        public const int    INVALID_ROW      = -1;
        public const int    INVALID_COL      = -1;
        
        public const string DEFAULT_NODE     = "game";

        
        public const int PERSISTID_PROTO_IDENT = 1;
        public const int PERSISTID_PROTO_SERIAL = PERSISTID_PROTO_IDENT + 1;

        public const int PERSISTID_MEMBERS_SIZE = 2;
        public const char PERSISTID_MEMBERS_SPLIT_FLAG = ':';
        
        public const int INT_PROTO_X = 1;
        public const int INT_PROTO_Y = INT_PROTO_X + 1;
        public const int INT_PROTO_Z = INT_PROTO_Y + 1;

        public const int VARIANT_PROTO_BOOL_TYPE       = 1;
        public const int VARIANT_PROTO_BOOL_VALUE      = VARIANT_PROTO_BOOL_TYPE       + 1;
        public const int VARIANT_PROTO_BYTE_TYPE       = VARIANT_PROTO_BOOL_VALUE      + 1;
        public const int VARIANT_PROTO_BYTE_VALUE      = VARIANT_PROTO_BYTE_TYPE       + 1;
        public const int VARIANT_PROTO_INT_TYPE        = VARIANT_PROTO_BYTE_VALUE      + 1;
        public const int VARIANT_PROTO_INT_VALUE       = VARIANT_PROTO_INT_TYPE        + 1;
        public const int VARIANT_PROTO_FLOAT_TYPE      = VARIANT_PROTO_INT_VALUE       + 1;
        public const int VARIANT_PROTO_FLOAT_VALUE     = VARIANT_PROTO_FLOAT_TYPE      + 1;
        public const int VARIANT_PROTO_LONG_TYPE       = VARIANT_PROTO_FLOAT_VALUE     + 1;
        public const int VARIANT_PROTO_LONG_VALUE      = VARIANT_PROTO_LONG_TYPE       + 1;
        public const int VARIANT_PROTO_STRING_TYPE     = VARIANT_PROTO_LONG_VALUE      + 1;
        public const int VARIANT_PROTO_STRING_VALUE    = VARIANT_PROTO_STRING_TYPE     + 1;
        public const int VARIANT_PROTO_PID_TYPE        = VARIANT_PROTO_STRING_VALUE    + 1;
        public const int VARIANT_PROTO_PID_VALUE       = VARIANT_PROTO_PID_TYPE        + 1;
        public const int VARIANT_PROTO_BYTES_TYPE      = VARIANT_PROTO_PID_VALUE       + 1;
        public const int VARIANT_PROTO_BYTES_VALUE     = VARIANT_PROTO_BYTES_TYPE      + 1;
        public const int VARIANT_PROTO_INT2_TYPE       = VARIANT_PROTO_BYTES_VALUE     + 1;
        public const int VARIANT_PROTO_INT2_VALUE      = VARIANT_PROTO_INT2_TYPE       + 1;
        public const int VARIANT_PROTO_INT3_TYPE       = VARIANT_PROTO_INT2_VALUE      + 1;
        public const int VARIANT_PROTO_INT3_VALUE      = VARIANT_PROTO_INT3_TYPE       + 1;
        
        public const int DATA_PROTO_ENITITY            = 1;
        public const int DATA_PROTO_ENTITY_TYPE        = DATA_PROTO_ENITITY + 1;
        public const int DATA_PROTO_ENTITY_SELF_PID    = DATA_PROTO_ENTITY_TYPE + 1;
        public const int DATA_PROTO_ENTITY_PROPERTIES  = DATA_PROTO_ENTITY_SELF_PID + 1;
        public const int DATA_PROTO_ENTITY_RECORDS     = DATA_PROTO_ENTITY_PROPERTIES + 1;
        public const int DATA_PROTO_ENTITY_CONTAINERS  = DATA_PROTO_ENTITY_RECORDS + 1;
        
        public const int DATA_PROTO_PROPERTY_TYPE      = DATA_PROTO_ENTITY_CONTAINERS  + 1;
        public const int DATA_PROTO_PROPERTY_VARIANT   = DATA_PROTO_PROPERTY_TYPE      + 1;
        public const int DATA_PROTO_PROPERTY_NAME      = DATA_PROTO_PROPERTY_VARIANT   + 1;

        public const int DATA_PROTO_RECORD_TYPE        = DATA_PROTO_PROPERTY_NAME      + 1;
        public const int DATA_PROTO_RECORD_NAME        = DATA_PROTO_RECORD_TYPE        + 1;
        public const int DATA_PROTO_RECORD_COLS        = DATA_PROTO_RECORD_NAME        + 1;
        public const int DATA_PROTO_RECORD_MAX_ROW     = DATA_PROTO_RECORD_COLS        + 1;
        public const int DATA_PROTO_RECORD_ROW_VALUES  = DATA_PROTO_RECORD_MAX_ROW     + 1;
        public const int DATA_PROTO_RECORD_USED_ROWS   = DATA_PROTO_RECORD_ROW_VALUES  + 1;

        public const int DATA_PROTO_CONTAINER_TYPE     = DATA_PROTO_RECORD_USED_ROWS   + 1;
        public const int DATA_PROTO_CONTAINER_NAME     = DATA_PROTO_CONTAINER_TYPE     + 1;
        public const int DATA_PROTO_CONTAINER_CAPACITY = DATA_PROTO_CONTAINER_NAME     + 1;
        public const int DATA_PROTO_CONTAINER_SLOTS    = DATA_PROTO_CONTAINER_CAPACITY + 1;
        public const int DATA_PROTO_CONTAINER_PIDS     = DATA_PROTO_CONTAINER_SLOTS    + 1;
    }

    public enum VariantType : byte
    {
        None = 0,

        /// <summary>
        /// variant`s type is bool
        /// </summary>
        Bool = 1,

        /// <summary>
        /// variant`s type is byte
        /// </summary>
        Byte = 2,

        /// <summary>
        /// variant`s type is int
        /// </summary>
        Int = 3,

        /// <summary>
        /// variant`s type is float
        /// </summary>
        Float = 4,

        /// <summary>
        /// variant`s type is long
        /// </summary>
        Long = 5,

        /// <summary>
        /// variant`s type is string
        /// </summary>
        String = 6,

        /// <summary>
        /// variant`s type is pid
        /// </summary>
        PersistID = 7,

        /// <summary>
        /// variant`s type is bytes
        /// </summary>
        Bytes = 8,

        /// <summary>
        /// variant`s type is Int2
        /// </summary>
        Int2 = 9,

        /// <summary>
        /// variant`s type is Int3
        /// </summary>
        Int3 = 10,
    }
}