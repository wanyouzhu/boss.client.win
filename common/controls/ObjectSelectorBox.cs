using System.Collections.Generic;
using System.Windows.Input;
using Autofac;

namespace boss.client.win.common.controls
{
    public class ObjectSelectorBox : TextBasedInputBox
    {
        private readonly IQueryService queryService;
        private readonly string queryCode;
        private string invalidValueText = "??????";

        public ObjectSelectorBox(Parameter parameter) : base(parameter)
        {
            queryService = ApplicationContext.ComponentContext.Resolve<IQueryService>();
            queryCode = parameter.subQueryCode;
            CommandBindings.Add(new CommandBinding(InputBox.Browse, BrowseCommandExecuted));
            InputBindings.Add(new KeyBinding(InputBox.Browse, Key.PageDown, ModifierKeys.None));
        }

        private void BrowseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SelectObjectWithBrowser();
        }

        private void SelectObjectWithBrowser()
        {
            var browser = new ObjectBrowser(queryCode, IsKeyboardFocused ? Text.Trim() : string.Empty);
            if (browser.ShowDialog() != true) return;
            ChangeFullValue(browser.Result);
        }

        protected override object ParseFullValue()
        {
            return QueryObjectByCode(Text?.Trim())?.data;
        }

        protected override object ExtractValue(object fullValue)
        {
            if (fullValue == null) return null;
            return (string)((dynamic)fullValue)._id;
        }

        protected override string ToInputText()
        {
            return FullValue == null ? string.Empty : (string)((dynamic)FullValue).code;
        }

        protected override string ToDisplayText()
        {
            if (FullValue == null) return invalidValueText;
            dynamic obj = FullValue;
            return $"[{obj.code}] {obj.name}";
        }

        private SingleResult QueryObjectByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return new SingleResult();
            return queryService.ExecuteQueryAsSingle(queryCode, new Dictionary<string, object> { { "_code", code } });
        }
    }
}