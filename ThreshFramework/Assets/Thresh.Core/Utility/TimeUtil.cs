using System;

namespace Thresh.Core.Utility
{
    public static class TimeUtil
    {
        private static DateTime _DegineDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long DeginTicks = _DegineDateTime.Ticks;

        public static long Offset = 0;

        public static long NowMilliseconds
        {
            get { return (DateTime.Now.ToUniversalTime().Ticks - DeginTicks) / TimeSpan.TicksPerMillisecond + Offset; }
        }
        public static long NextReflushTime
        {
            get
            {
                return (DateTime.Now.ToUniversalTime().Ticks-DeginTicks)/TimeSpan.TicksPerMillisecond+ Offset;
            }
        }
        public static DateTime Now
        {
            get
            {
                return DateTime.Now.ToUniversalTime();
            }
        }

        public static DateTime TimestampToDataTime(long ts)
        {
            return _DegineDateTime.AddMilliseconds(ts);
        }

        public static bool IsSameDay(DateTime dt1, DateTime dt2)
        {
            return dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day;
        }

        public static bool IsSameDay(long ms1, long ms2)
        {
            DateTime dt1 = TimestampToDataTime(ms1);
            DateTime dt2 = TimestampToDataTime(ms2);
            return IsSameDay(dt1, dt2);
        }

        public static void Collabrate(long ts)
        {
            Offset += ts - NowMilliseconds;
        }

        private const long checktime1 = 3600 * 12;
        private const long checktime2 = 3600 * 18;

        public static bool CheckResetTime(long ntime)
        {
            long time = NowMilliseconds;
            long day = ntime / 86400;
            long nowday = time / 86400;
            if (nowday != day)
            {
                return true;
            }
            else
            {
                long h = ntime % 86400;
                long nh = time % 86400;
                if (nh>=checktime1 && h < checktime1)
                {
                    return true;
                }else if (nh >= checktime2 )
                {
                    return true;
                }
            }

            return false;
        }

    }
}