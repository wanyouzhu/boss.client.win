using System.Windows;
using System.Windows.Controls;

namespace boss.client.win
{
    public class Page : UserControl
    {
        public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
            "Code", typeof(string), typeof(Page), new PropertyMetadata(default(string)));

        public string Code
        {
            get { return (string) GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(Page), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
            "ToolBar", typeof(FrameworkElement), typeof(Page), new PropertyMetadata(default(FrameworkElement)));

        public FrameworkElement ToolBar
        {
            get { return (FrameworkElement) GetValue(ToolBarProperty); }
            set { SetValue(ToolBarProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(string), typeof(Page), new PropertyMetadata(default(string)));

        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}