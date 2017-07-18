namespace boss.client.win.common.controls
{
    public class NumberInputBox : TextBasedInputBox
    {
        public NumberInputBox(Parameter parameter) : base(parameter)
        {
        }

        protected override object ParseFullValue()
        {
            return Text.Trim() ?? string.Empty;
        }

        protected override string ToInputText()
        {
            return Value as string ?? string.Empty;
        }

        protected override string ToDisplayText()
        {
            return Value as string ?? string.Empty;
        }
    }
}