using System;
using Thresh.Core.Config.INI;
using Thresh.Core.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Thresh.Unity.App
{
    public enum UserSetting{
        Tonemapping,//色彩映射
        ScreenSpaceAmbientOcclusion,//空间环境光
        VignetteAndChromaticAberration,//色差
        ColorCorrectionCurves,//颜色校正
        Bloom,//阳光斑耀
        ScreenSpaceAmbientObscurance,//空间环境
        EdgeDetection,//描边
        GlobalFog,//体积雾
    }
    
    public class UserinfoApp : AppBase
    {
        public string Token
        {
            get
            {
                //return "fa3ddb64-3e04-4a2c-958f-0e9196cb713e";
                return GetToken(Uuid);
            }
            set
            {
                SetToken(Uuid, value);
            }
        }
        
        public string Uuid
        {
            get
            {
                return GetToken(Uuid);
            }
            set
            {
                SetToken(Uuid,value);
            }
        }
        
        public override void OnAwake(Configuration app_ini)
        {
            try
            {
#if UNITY_EDITOR
                if (!string.IsNullOrEmpty(app_ini["account"]["uuid"].StringValue))
                {
                    Uuid = app_ini["account"]["uuid"].StringValue;
                }
#else
                if (string.IsNullOrEmpty(Uuid))
                {
                    Uuid = app_ini["account"]["uuid"].StringValue;
                }
#endif
                Logger.Debug(app_ini["account"]["uuid"].StringValue);
                if (string.IsNullOrEmpty(Uuid))
                {
                    Uuid = SystemInfo.deviceUniqueIdentifier;
                }
            }
            catch (Exception e)
            {
                throw new INIException("UserApp read uuid fail", e);
            }
        }

        public override void OnOpen()
        {
            
        }

        public string GetToken(string uuid)
        {
            return PlayerPrefs.GetString("uuid_" + uuid);
        }

        public void SetToken(string uuid, string token)
        {
            PlayerPrefs.SetString("uuid_"+uuid,token);
            PlayerPrefs.Save();
        }

        public int GetUserSetting(string setting, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(setting, defaultValue);
        }

        public void SetUserSetting(string setting, int value)
        {
            PlayerPrefs.SetInt(setting,value);
            PlayerPrefs.Save();
        }

        public int GetUserClientCache(string name, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(Uuid + name, defaultValue);
        }

        public void SetUserClientCache(string name, int value)
        {
            PlayerPrefs.SetInt(Uuid+name,value);
            PlayerPrefs.Save();
        }
        
        public float GetFloatUserValue(string name,float defaultValue = 1)
        {
            return PlayerPrefs.GetFloat(name,defaultValue);
        }

        public void SetFloatUserValue(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
            PlayerPrefs.Save();
        }
        
        public string SessionKey
        {
            get
            {
                return GetSessionKey();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetSessionKey(value);
                }
            }
        }
        
        public void SetEntity(PersistID pid, string json)
        {
            PlayerPrefs.SetString(pid.ToString(), json);
        }

        public string GetEntity(PersistID pid)
        {
            return PlayerPrefs.GetString(pid.ToString());
        }
        public string GetAccount()
        {
            string account = PlayerPrefs.GetString("account");
            return account;
        }
        public void SetAccount(string account)
        {
            PlayerPrefs.SetString("account",account);
            PlayerPrefs.Save();
        }
        public void SetPassword(string password)
        {
            PlayerPrefs.SetString("password", password);
            PlayerPrefs.Save();
        }
        public string GetPassword()
        {
            string password = PlayerPrefs.GetString("password");
            if (password == "")
                return GetToken(GetAccount());
            return password;
        }
        public string GetUuid()
        {
            return PlayerPrefs.GetString("uuid");
        }

        public string GetSessionKey()
        {
            return PlayerPrefs.GetString("session_key");
        }

        public void SetUuid(string uuid)
        {
            PlayerPrefs.SetString("uuid", uuid);
            PlayerPrefs.Save();
        }

        public void SetSessionKey(string session_key)
        {
            PlayerPrefs.SetString("session_key", session_key);
            PlayerPrefs.Save();
        }

        public void StartGame()
        {
            int num = PlayerPrefs.GetInt("start_game");
            PlayerPrefs.SetInt("start_game", num + 1);
            PlayerPrefs.Save();
        }

        public bool IsFirstGame()
        {
            return PlayerPrefs.GetInt("start_game") == 0;
        }

        public override void OnClose()
        {
            throw new System.NotImplementedException();
        }
    }
}