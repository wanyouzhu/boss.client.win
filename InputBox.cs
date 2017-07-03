using System.Windows;
using System.Windows.Controls;

namespace boss.client.win
{
    public class InputBox
    {
        public static FrameworkElement Of(QueryParameter parameter)
        {
            switch (parameter.type)
            {
                case ParameterTypes.Object: return new ObjectSelectorBox();
                case ParameterTypes.Text: return new TextInputBox();
                case ParameterTypes.Number: return new NumberInputBox();
                case ParameterTypes.DateTime: return new DateTimeInputBox();
                case ParameterTypes.Date: return new DateInputBox();
                case ParameterTypes.Time: return new TimeInputBox();
                default: throw new ApplicationException("message.error.unknown-parameter-type", parameter.type);
            }
        }
    }

    public class TimeInputBox : TextBox, IInputBox
    {
    }

    public class DateInputBox : TextBox, IInputBox
    {
    }

    public class DateTimeInputBox : TextBox, IInputBox
    {
    }

    public class NumberInputBox : TextBox, IInputBox
    {
    }

    public class ObjectSelectorBox : TextBox, IInputBox
    {
    }

    public class TextInputBox : TextBox, IInputBox
    {
    }

    public interface IInputBox
    {
    }
}