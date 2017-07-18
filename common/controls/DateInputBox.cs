using System;

namespace boss.client.win.common.controls
{
    public class DateInputBox : TextBasedInputBox
    {
        public DateInputBox(Parameter parameter) : base(parameter)
        {
        }

        protected override object ParseFullValue()
        {
            var text = Text?.Trim();
            if (text == null) return null;
            return DateTime.Parse(text).Date;
        }

        protected override string ToInputText()
        {
            return Value == null ? string.Empty : ((Value as DateTime?)?.ToString("yyyy-MM-dd") ?? string.Empty);
        }

        protected override string ToDisplayText()
        {
            return (Value as DateTime?)?.ToString("yyyy-MM-dd") ?? string.Empty;
        }
    }
}