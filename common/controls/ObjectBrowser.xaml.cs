using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace boss.client.win.common.controls
{
    public partial class ObjectBrowser
    {

        private readonly string initialInput;
        private readonly ObjectBrowserViewModel viewModel;

        public ObjectBrowser(string queryCode, string initialInput)
        {
            this.initialInput = initialInput;
            viewModel = new ObjectBrowserViewModel(queryCode, initialInput);
            Owner = Application.Current.GetActiveWindow();
            InitializeComponent();
            DataContext = viewModel;
        }

        public object Result
        {
            get; set;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            viewModel.Load();
            foreach (var column in viewModel.Columns)
            {
                grid.Columns.Add(column);
            }
        }

        private void ObjectBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            ResolveFocus(true);
            Query();
        }

        private void ObjectBrowser_OnUnloaded(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
        }

        private void Grid_OnSorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;
            var multiSort = IsKeyDown(Key.LeftShift) || IsKeyDown(Key.RightShift);
            viewModel.SortField(e.Column.SortMemberPath, multiSort);
        }

        private static bool IsKeyDown(Key key)
        {
            return (Keyboard.GetKeyStates(key) & KeyStates.Down) != 0;
        }

        private void firstPage_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.GotoFirstPage();
        }

        private void previousPage_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.GotoPreviousPage();
        }

        private void nextPage_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.GotoNextPage();
        }

        private void lastPage_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.GotoLastPage();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            conditionHost.Content = viewModel.GetCurrentParameterBox() as FrameworkElement;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            viewModel.Query();
            ResolveFocus(false);
        }

        private void ResolveFocus(bool initial)
        {
            if (grid.Items.Count < 1 || grid.Columns.Count < 1 || (initial && string.IsNullOrWhiteSpace(initialInput)))
            {
                (viewModel.GetCurrentParameterBox() as Control)?.Focus();
            }
            else
            {
                grid.SelectedIndex = 0;
                grid.Focus();
                grid.CurrentCell = new DataGridCellInfo(grid.SelectedItem, grid.Columns.First());
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key != Key.Enter) return;
            if ((e.Source as IInputBox) == null) return;
            e.Handled = true;
            Query();
        }

        private void Grid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (grid.SelectedItem == null) return;
            e.Handled = true;
            Result = grid.SelectedItem;
            DialogResult = true;
        }

        private void Grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grid.SelectedItem == null) return;
            e.Handled = true;
            Result = grid.SelectedItem;
            DialogResult = true;
        }
    }
}
