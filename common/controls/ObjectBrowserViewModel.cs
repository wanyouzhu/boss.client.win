using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace boss.client.win.common.controls
{
    public class ObjectBrowserViewModel : AbstractQueryViewModel
    {
        private readonly string initialValue;

        public ObjectBrowserViewModel(string queryCode, string initialValue) : base(queryCode, $"ui.objselector.{queryCode}")
        {
            this.initialValue = initialValue;
            PageSize = 10;
        }

        public static readonly DependencyProperty SelectedParameterIndexProperty = DependencyProperty.Register(
            "SelectedParameterIndex", typeof(int), typeof(ObjectBrowserViewModel), new PropertyMetadata(default(int)));

        public int SelectedParameterIndex
        {
            get { return (int)GetValue(SelectedParameterIndexProperty); }
            set { SetValue(SelectedParameterIndexProperty, value); }
        }

        public static readonly DependencyProperty LabelsProperty = DependencyProperty.Register(
            "Labels", typeof(IEnumerable<FrameworkElement>), typeof(ObjectBrowserViewModel), new PropertyMetadata(default(IEnumerable<FrameworkElement>)));

        public IEnumerable<FrameworkElement> Labels
        {
            get { return (IEnumerable<FrameworkElement>)GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }


        protected override FrameworkElement CreateParameterContent(Parameter parameter)
        {
            return new TextInputBox(new Parameter {name = parameter.name});
        }

        protected override FrameworkElement CreateParameterHeader(Parameter parameter)
        {
            return new TextBlock() { Text = parameter.description };
        }

        protected override IDictionary<string, object> GetQueryArguments()
        {
            var parameter = GetCurrentParameterBox();
            if (parameter == null) return new Dictionary<string, object>();
            return new Dictionary<string, object>() { { parameter.GetName(), parameter.GetValue() } };
        }

        public IInputBox GetCurrentParameterBox()
        {
            return GetInputBoxes().Skip(SelectedParameterIndex).First();
        }

        protected override void HandleProfileLoaded()
        {
            Labels = Items.Where((x, i) => i % 2 == 0).ToList();
            SelectedParameterIndex = 0;
            GetCurrentParameterBox().ChangeValue(initialValue);
            CurrentPage = 0;
        }
    }
}