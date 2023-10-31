using System;

namespace Ecommerce.Common.Utils
{
    public static class DateTimeUtils
    {
        private static readonly DateTime UnixBase = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static readonly string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public static long ToUnixTimeStampUniversalMilliseconds(this DateTime dateTime)
        {
            return (long) (dateTime.ToUniversalTime() - UnixBase).TotalMilliseconds;
        }

        public static long ToUnixTimeStampMilliseconds(this DateTime dateTime)
        {
            return (long) (dateTime - UnixBase).TotalMilliseconds;
        }

        public static long ToUnixTimeStampSeconds(this DateTime dateTime)
        {
            return (long) (dateTime - UnixBase).TotalSeconds;
        }

        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString(DateFormat);
        }

        public static DateTime ToDateTime(this long utc)
        {
            return UnixBase.AddMilliseconds(utc);
        }

        public static DateTime MaxBetween(DateTime first, DateTime second)
        {
            return first > second ? first : second;
        }

        public static DateTime MinBetween(DateTime first, DateTime second)
        {
            return first < second ? first : second;
        }

        public static int GetDifferenceInDays(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!dateFrom.HasValue || !dateTo.HasValue) return 0;

            return Convert.ToInt32(Math.Ceiling((dateTo.Value.Date - dateFrom.Value.Date).TotalDays));
        }
        
        /// <summary>
        /// Check if DateTime is expired
        /// </summary>
        public static bool IsExpired(this DateTime date)
        {
            return date < DateTime.Now.AddMinutes(1);
        }

        public static DateTime RoundToSeconds(this DateTime date)
        {
            return date.AddTicks( - (date.Ticks % TimeSpan.TicksPerSecond));
        }

        public static DateTime GetUtcNowRoundToSeconds()
        {
            return DateTime.UtcNow.RoundToSeconds();
        }

        public static DateTime GetUtcDateByIsoString(this string dateTimeIsoString)
        {
            return DateTime.Parse(dateTimeIsoString);
        }
    }
}
