using System.Windows;
using System.Windows.Controls;

namespace boss.client.win
{
    public class PageTabItem : TabItem
    {
        public PageTabItem()
        {
            IsClosable = true;
        }

        public PageTabItem(MenuItem menuItem, object parameter) : this()
        {
            Type = menuItem.Type;
            Code = menuItem.Code;
            Title = menuItem.Name;
            Header = menuItem.Name;
            Parameter = parameter;
        }

        public string Type
        {
            get;
        }

        public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
            "Code", typeof(string), typeof(PageTabItem), new PropertyMetadata(default(string)));

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            private set { SetValue(CodeProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(PageTabItem), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            private set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register(
            "IsClosable", typeof(bool), typeof(PageTabItem), new PropertyMetadata(default(bool)));

        public bool IsClosable
        {
            get { return (bool)GetValue(IsClosableProperty); }
            private set { SetValue(IsClosableProperty, value); }
        }

        public object Parameter
        {
            get;
        }

        public bool IsBelongTo(MenuItem menuItem, object parameter)
        {
            return string.Equals(Type, menuItem.Type) && string.Equals(Code, menuItem.Code) && Equals(Parameter, parameter);
        }
    }
}