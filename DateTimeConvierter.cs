using System;
using System.Globalization;
using System.Windows.Data;
using Newtonsoft.Json.Linq;

namespace boss.client.win
{
    public class DateTimeConvierter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FormatDateTime(value as DateTime? ?? UnixTimeStampToDateTime(value as long?) ?? JValueToDateTime(value as JValue));
        }

        private static string FormatDateTime(DateTime? dateTime)
        {
            return dateTime?.ToLocalTime().Format();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string)value).ToUniversalTime();
        }

        private static DateTime? UnixTimeStampToDateTime(long? unixTimeStamp)
        {
            if (unixTimeStamp == null) return null;
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value / 1000.0).ToLocalTime();
            return dtDateTime;
        }

        private static DateTime? JValueToDateTime(JValue jValue)
        {
            if (jValue == null) return null;
            switch (jValue.Type)
            {
                case JTokenType.Integer: return UnixTimeStampToDateTime(jValue.Value<long>());
                case JTokenType.Date: return jValue.Value<DateTime>();
                default: return null;
            }
        }
    }
}