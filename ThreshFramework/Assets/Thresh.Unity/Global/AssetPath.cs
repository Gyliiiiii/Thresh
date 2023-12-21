using UnityEngine;

namespace Thresh.Unity.Global
{
    public static class AssetPath
    {
        public static string ResourcePath
        {
            get
            {
                #if UNITY_EDITOR
                return Application.dataPath + "/Build/";
#else
                return PersistentDataPath;
#endif
            }
        }
        
        public static string PersistentDataPath
        {
            get { return Application.persistentDataPath + "/"; }
        }
        
        public static string StreamingPath
        {
            get { return Application.streamingAssetsPath + "/"; }
        }
        
        public static string BundlePath
        {
            get { return ResourcePath + "bundle/"; }
        }
        
        public static string LuaPath
        {
            get { return ResourcePath + "config/lua/"; }
        }
        
        public static string BehaviacPath
        {
            get { return ResourcePath + "config/behaviac/"; }
        }
        
        public static string GameConfigPath
        {
            get { return ResourcePath + "config/"; }
        }
        
        public static string LocalizationPath
        {
            get { return GameConfigPath + "localization/"; }
        }
    }
}