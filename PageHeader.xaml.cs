using System.Windows;

namespace boss.client.win
{
    public partial class PageHeader
    {
        public PageHeader()
        {
            DataContext = this;
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(PageHeader), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
            "ToolBar", typeof(FrameworkElement), typeof(PageHeader), new PropertyMetadata(default(FrameworkElement)));

        public FrameworkElement ToolBar
        {
            get { return (FrameworkElement) GetValue(ToolBarProperty); }
            set { SetValue(ToolBarProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(string), typeof(PageHeader), new PropertyMetadata(default(string)));

        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationEvents.OnCloseActivePageRequested();
        }
    }
}
