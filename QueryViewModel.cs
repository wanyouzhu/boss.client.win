using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using boss.client.win.common.controls;

namespace boss.client.win
{
    public class QueryViewModel : AbstractQueryViewModel
    {
        private readonly string labelTail;

        public QueryViewModel(string queryCode) : base(queryCode, $"ui.{queryCode}")
        {
            labelTail = Application.Current.TryFindResource("message.label.tail") as string ?? string.Empty;
        }

        protected override FrameworkElement CreateParameterContent(Parameter parameter)
        {
            return InputBox.Of(parameter);
        }

        protected override FrameworkElement CreateParameterHeader(Parameter parameter)
        {
            return new Label() { Content = parameter.description + labelTail };
        }

        protected override IDictionary<string, object> GetQueryArguments()
        {
            var inputBoxes = GetInputBoxes();
            return inputBoxes.ToDictionary(x => x.GetName(), x => x.GetValue());
        }
    }
}