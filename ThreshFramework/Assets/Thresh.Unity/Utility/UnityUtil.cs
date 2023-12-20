using Thresh.Core.Interface;
using UnityEngine;

namespace Thresh.Unity.Utility
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        protected ILog Logger;
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    System.Type type = typeof(T);
                    obj.name = "[Singleton][" + type.Name + "]";
                    _instance = obj.AddComponent<T>();

                    _instance.Logger = LogAssert.GetLog(obj.name);
                }
                return _instance;
            }
        }
    }
} 