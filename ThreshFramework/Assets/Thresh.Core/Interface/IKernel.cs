using Thresh.Core.Data;

namespace Thresh.Core.Interface
{
    public static class AttributeExtend
    {
        public const string AttributeInt = "attribute_int";
        public const string AttributeString = "attribute_string";
    }
    
    public interface IKernel
    {
        #region ----- ----- ----- ----- Core ----- ----- ----- -----

        EntityDef getEntityDef(string type);


        #endregion
    }
}