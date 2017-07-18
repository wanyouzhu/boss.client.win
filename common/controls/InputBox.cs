using System.Windows;
using System.Windows.Input;

namespace boss.client.win.common.controls
{
    public interface IInputBox
    {
        string GetName();
        object GetValue();
        void ClearValue();
        void ChangeValue(object value);
        void ChangeFullValue(object fullValue);
    }

    public class InputBox
    {
        public static readonly RoutedCommand Browse = new RoutedCommand("Browse", typeof(InputBox));

        public static FrameworkElement Of(Parameter parameter)
        {
            switch (parameter.type)
            {
                case ParameterTypes.Object: return new ObjectSelectorBox(parameter);
                case ParameterTypes.Text: return new TextInputBox(parameter);
                case ParameterTypes.Number: return new NumberInputBox(parameter);
                case ParameterTypes.DateTime: return new DateTimeInputBox(parameter);
                case ParameterTypes.Date: return new DateInputBox(parameter);
                default: throw new ApplicationException("message.error.unknown-parameter-type", parameter.type);
            }
        }
    }
}