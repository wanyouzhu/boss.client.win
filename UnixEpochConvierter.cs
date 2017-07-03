using System;
using System.Globalization;
using System.Windows.Data;

namespace boss.client.win
{
    public class UnixEpochConvierter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            dynamic epoch = value;
            return UnixTimeStampToDateTime(epoch == null ? 0L : epoch.Value).ToString("yyyy-MM-dd HH:ss:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime.Parse((string)value) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalMilliseconds * 1000;
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp / 1000.0).ToLocalTime();
            return dtDateTime;
        }
    }
}