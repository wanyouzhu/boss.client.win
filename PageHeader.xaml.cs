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

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationEvents.OnCloseActivePageRequested();
        }
    }
}
