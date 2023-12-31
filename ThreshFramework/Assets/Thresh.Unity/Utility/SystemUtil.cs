﻿using System;
using System.Collections.Generic;
using Thresh.Unity.Global;

namespace Thresh.Unity.Utility
{
    public static class SystemUtil
    {
        public static int FloatToInitBits(float f)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
        }

        public static float InitBitsToFloat(int i)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
        }

        public static void StringToList<T>(ref List<T> ret, string s, char sep = ',') where T : IConvertible
        {
            ret.Clear();
            string[] ss = s.Split(sep);
            for (int i = 0; i < ss.Length; i++)
            {
                try
                {
                    T v = (T)Convert.ChangeType(ss[i], typeof(T));
                    ret.Add(v);
                }
                catch (Exception e)
                {
                    LogAssert.Util.Warn("Convert \"{0}\" to {1} List error.({2})",s,typeof(T).Name,e.ToString());
                }
            }
        }
    }
}