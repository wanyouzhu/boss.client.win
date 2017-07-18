namespace boss.client.win.common.controls
{
    public class TextInputBox : TextBasedInputBox
    {
        public TextInputBox(Parameter parameter) : base(parameter)
        {
        }

        protected override object ParseFullValue()
        {
            return Text?.Trim() ?? string.Empty;
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