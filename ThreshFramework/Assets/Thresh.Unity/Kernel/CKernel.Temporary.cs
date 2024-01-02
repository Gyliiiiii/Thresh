using Thresh.Core.Data;
using Thresh.Core.Interface;

namespace Thresh.Unity.Kernel
{
    partial class CKernel
    {
         public bool FindTemporary(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return false;
            }

            return entity.FindTemporary(temporary_name);
        }

        #region ------ ------ ------ ------Get------ ------ ------ ------
        public bool GetTemporaryBool(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_BOOLEAN;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_BOOLEAN;
            }

            return temporary.GetBool();
        }

        public byte GetTemporaryByte(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_BYTE;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_BYTE;
            }

            return temporary.GetByte();
        }

        public float GetTemporaryFloat(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_FLOAT;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_FLOAT;
            }

            return temporary.GetFloat();
        }

        public int GetTemporaryInt(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_INTEGER;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_INTEGER;
            }

            return temporary.GetInt();
        }

        public long GetTemporaryLong(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_LONG;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_LONG;
            }

            return temporary.GetLong();
        }

        public PersistID GetTemporaryPid(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return PersistID.Empty;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return PersistID.Empty;
            }

            return temporary.GetPid();
        }

        public string GetTemporaryString(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Constant.NULL_STRING;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Constant.NULL_STRING;
            }

            return temporary.GetString();
        }

        public Int2 GetTemporaryInt2(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Int2.Zero;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Int2.Zero;
            }

            return temporary.GetInt2();
        }

        public Int3 GetTemporaryInt3(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Int3.Zero;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Int3.Zero;
            }

            return temporary.GetInt3();
        }

        public Bytes GetTemporaryBytes(PersistID pid, string temporary_name)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return Bytes.Zero;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                return Bytes.Zero;
            }

            return temporary.GetBytes();
        }
        #endregion

        #region ------ ------ ------ ------Set------ ------ ------ ------
        public void SetTemporaryBool(PersistID pid, string temporary_name, bool temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Bool);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetBool(temporary_value);
        }

        public void SetTemporaryByte(PersistID pid, string temporary_name, byte temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Byte);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetByte(temporary_value);
        }

        public void SetTemporaryFloat(PersistID pid, string temporary_name, float temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Float);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetFloat(temporary_value);
        }

        public void SetTemporaryInt(PersistID pid, string temporary_name, int temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Int);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetInt(temporary_value);
        }

        public void SetTemporaryLong(PersistID pid, string temporary_name, long temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Long);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetLong(temporary_value);
        }

        public void SetTemporaryPid(PersistID pid, string temporary_name, PersistID temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.PersistID);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetPid(temporary_value);
        }

        public void SetTemporaryString(PersistID pid, string temporary_name, string temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.String);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetString(temporary_value);
        }

        public void SetTemporaryBytes(PersistID pid, string temporary_name, Bytes temporary_value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Bytes);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetBytes(temporary_value);
        }


        public void SetTemporaryInt2(PersistID pid, string temporary_name, Int2 value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Int2);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetInt2(value);
        }

        public void SetTemporaryInt3(PersistID pid, string temporary_name, Int3 value)
        {
            IEntity entity = _EntityManager.GetEntity(pid);
            if (entity == null)
            {
                return;
            }

            ITemporary temporary = entity.GetTemporary(temporary_name);
            if (temporary == null)
            {
                temporary = entity.CreateTemporary(temporary_name, VariantType.Int3);
            }

            if (temporary == null)
            {
                return;
            }

            temporary.SetInt3(value);
        }
        #endregion
    }
}