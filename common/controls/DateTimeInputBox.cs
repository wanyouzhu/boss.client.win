using System;
using System.Globalization;

namespace boss.client.win.common.controls
{
    public class DateTimeInputBox : TextBasedInputBox
    {
        private string displayFormat = "yyyy-MM-dd HH:mm:ss";
        private string invalidValueText = "??????";
        private static readonly string[] formats =
        {
            "yyyy-MM-dd HH:mm:ss.ffffff", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH", "yyyy-MM-dd",
            "yyyy/MM/dd HH:mm:ss.ffffff", "yyyy/MM/dd HH:mm:ss", "yyyy/MM/dd HH:mm", "yyyy/MM/dd HH", "yyyy/MM/dd",
            "yyyyMMddHHmmssfffffff", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddHH", "yyyyMMddHH", "yyyyMMdd"
        };

        public DateTimeInputBox(Parameter parameter) : base(parameter)
        {
        }

        protected override object ParseFullValue()
        {
            var text = Text?.Trim();
            if (text == null) return null;
            if (string.Equals(text, invalidValueText)) return null;
            DateTime result;
            return DateTime.TryParseExact(text, formats, CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out result) ? result as object : null;
        }

        protected override string ToInputText()
        {
            return FullValue == null ? string.Empty : ((FullValue as DateTime?)?.ToString(displayFormat) ?? string.Empty);
        }

        protected override string ToDisplayText()
        {
            return (FullValue as DateTime?)?.ToString(displayFormat) ?? invalidValueText;
        }
    }
}