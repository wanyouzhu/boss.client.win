using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autofac;

namespace boss.client.win
{
    public partial class StartMenuPageMenuContent
    {
        public StartMenuPageMenuContent()
        {
            DataContext = this;
            InitializeComponent();
            Items = ApplicationContext.ComponentContext.Resolve<IApplicationService>().GetMenuItems();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable<MenuItem>), typeof(StartMenuPageMenuContent), new PropertyMetadata(default(IEnumerable<MenuItem>)));

        public IEnumerable<MenuItem> Items
        {
            get { return (IEnumerable<MenuItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        private void GroupBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemBox.SelectedIndex = 0;
        }
    }
}
