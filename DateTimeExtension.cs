using System;

namespace boss.client.win
{
    public static class DateTimeExtension
    {
        private const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static string Format(this DateTime datetime)
        {
            return datetime.ToString(DatetimeFormat);
        }

        public static string Format(this DateTime? datetime)
        {
            return datetime?.ToString(DatetimeFormat);
        }
    }
}