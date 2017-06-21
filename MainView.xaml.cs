using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace boss.client.win
{
    public partial class MainView
    {
        private readonly ObservableCollection<Page> pages = new ObservableCollection<Page>();

        public MainView()
        {
            Pages = new ObservableCollection<PageTabItem>();
            DataContext = this;
            InitializeComponent();
            var masterItem = GetOrAddItem("我的工作台");
            var item = GetOrAddItem("销售单查询");
            GetOrAddItem("销售单查询1");
            GetOrAddItem("销售单查询2");
            GetOrAddItem("销售单查询3");
            GetOrAddItem("销售单查询4");
            GetOrAddItem("销售单查询5");
            GetOrAddItem("销售单查询6");
            ActiveItem = item;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        public ObservableCollection<PageTabItem> Pages
        {
            get;
        }

        private PageTabItem GetOrAddItem(string code)
        {
            var existingPage = Pages.FirstOrDefault(x => x.Code.Equals(code));
            if (existingPage != null) return existingPage;
            var item = CreateNewItem(code);
            Pages.Add(item);
            return item;
        }

        private PageTabItem CreateNewItem(string code)
        {
            var page = new WorkbenchPage() { Title = code };
            return new PageTabItem() { Code = code, Header = code, Content = page };
        }

        public static readonly DependencyProperty ActiveItemProperty = DependencyProperty.Register(
            "ActiveItem", typeof(PageTabItem), typeof(MainView), new PropertyMetadata(default(PageTabItem)));

        public PageTabItem ActiveItem
        {
            get { return (PageTabItem)GetValue(ActiveItemProperty); }
            set { SetValue(ActiveItemProperty, value); }
        }
    }
}
