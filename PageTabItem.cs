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

        public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
            "Code", typeof(string), typeof(PageTabItem), new PropertyMetadata(default(string)));

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(PageTabItem), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register(
            "IsClosable", typeof(bool), typeof(PageTabItem), new PropertyMetadata(default(bool)));

        public bool IsClosable
        {
            get { return (bool)GetValue(IsClosableProperty); }
            set { SetValue(IsClosableProperty, value); }
        }
    }
}