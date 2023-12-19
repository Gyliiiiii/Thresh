using System;

namespace Thresh.Core.Utility
{
    public class Singleton<T> where T : new()
    {
        protected static T sm_instance;

        public static T instance
        {
            get
            {
                if (sm_instance == null)
                {
                    sm_instance = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
                }
                return sm_instance;
            }
        }
    }
}