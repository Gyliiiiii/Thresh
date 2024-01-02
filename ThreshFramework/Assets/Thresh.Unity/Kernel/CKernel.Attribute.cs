using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Variant;

namespace Thresh.Unity.Kernel
{
    partial class CKernel
    {
       #region ------ ------ ------ ------Get------ ------ ------ ------
        public int GetAttributeInt(PersistID pid, string attribute_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_INTEGER;
            }

            if (FindProperty(pid, attribute_name))
            {
                return GetPropertyInt(pid, attribute_name);
            }
            int row = FindRecordRowString(pid, AttributeExtend.AttributeInt, 0, attribute_name);
            if (row != -1)
            {
                return GetRecordInt(pid, AttributeExtend.AttributeInt, row, 1);
            }
            return Constant.NULL_INTEGER;
        }



        public string GetAttributeString(PersistID pid, string attribute_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_STRING;
            }

            if (FindProperty(pid, attribute_name))
            {
                return GetPropertyString(pid, attribute_name);
            }
            int row = FindRecordRowString(pid, AttributeExtend.AttributeString, 0, attribute_name);
            if (row != -1)
            {
                return GetRecordString(pid, AttributeExtend.AttributeString, row, 1);
            }

            return Constant.NULL_STRING;
        }
        #endregion

        #region ------ ------ ------ ------Set------ ------ ------ ------
        public void SetAttributeInt(PersistID pid, string attribute_name, int attribute_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            if (FindProperty(pid, attribute_name))
            {
                SetPropertyInt(pid, attribute_name, attribute_value);
                return;
            }
            int row = FindRecordRowString(pid, AttributeExtend.AttributeInt, 0, attribute_name);
            if (row != -1)
            {
                SetRecordInt(pid, AttributeExtend.AttributeInt, row, 1, attribute_value);
                return;
            }
            AddRecordRow(pid, AttributeExtend.AttributeInt, VariantList.New().Append(attribute_name).Append(attribute_value));
        }

        public void SetAttributeString(PersistID pid, string attribute_name, string attribute_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            if (FindProperty(pid, attribute_name))
            {
                SetPropertyString(pid, attribute_name, attribute_value);
                return;
            }
            int row = FindRecordRowString(pid, AttributeExtend.AttributeString, 0, attribute_name);
            if (row != -1)
            {
                SetRecordString(pid, AttributeExtend.AttributeString, row, 1, attribute_value);
                return;
            }
            AddRecordRow(pid, AttributeExtend.AttributeString, VariantList.New().Append(attribute_name).Append(attribute_value));
        }

        #endregion
    }
}