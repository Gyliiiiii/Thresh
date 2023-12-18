using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Thresh.Core.Utility
{
    public static class MathUtil
    {

        /// <summary>
        /// 根据GUID获取16位的唯一字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (var b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            return string.Format("{0:x}", i - DateTime.Now.ToUniversalTime().Ticks);
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        private static Random rnd = new Random(GetRandomSeed());

        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public const int MAX_RANDOM = 10000000;

        public static int GetRandom()
        {
            return rnd.Next(0, MAX_RANDOM);
        }

        public static bool CheckRandom(int num)
        {
            return rnd.Next(0, MAX_RANDOM) <= num;
        }

        public static int GetRandom(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static List<int> GetRandoms(int min, int max, int count)
        {
            List<int> result = new List<int>();
            if (max - min +1 <= count)
            {
                for (int i = min; i <= max; i++)
                {
                    result.Add(i);
                }
            }else if (max - min + 1 > count)
            {
                while (result.Count < count)
                {
                    int rnd = GetRandom(min, max);
                    if (!result.Contains(rnd))
                    {
                        result.Add(rnd);
                    }
                }
            }

            return result;
        }

        public static T CalculateRandomObject<T>(List<T> list)
        {
            if (list == null)
            {
                return default(T);
            }

            if (list.Count <=0)
            {
                return default(T);
            }

            int result = rnd.Next(0, list.Count);

            return list[result];
        }
        
        //public static T CalculateWeightObject<T>(List<Group<T,int>> list)
    }
}