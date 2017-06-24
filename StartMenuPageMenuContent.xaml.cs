using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void MenuItem_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2) return;
            var menuItem = (sender as Grid)?.DataContext as MenuItem;
            if (menuItem == null) return;
            ApplicationEvents.OnMenuItemSelected(menuItem);
        }

        private void ItemBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            var menuItem = ItemBox.SelectedItem as MenuItem;
            if (menuItem == null) return;
            ApplicationEvents.OnMenuItemSelected(menuItem);
        }
    }
}
