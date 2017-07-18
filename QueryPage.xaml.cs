using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace boss.client.win
{
    [Page("__QUERY__")]
    public partial class QueryPage
    {
        private readonly QueryViewModel viewModel;

        public QueryPage(object parameter)
        {
            var queryCode = ((dynamic)parameter).queryCode as string;
            viewModel = new QueryViewModel(queryCode);
            DataContext = viewModel;
            InitializeComponent();
            inputForm.DataContext = viewModel;
        }

        private void QueryPage_OnInitialized(object sender, EventArgs e)
        {
            viewModel.Load();
            ReloadGridViewColumns();
        }

        private void ReloadGridViewColumns()
        {
            gridView.Columns.Clear();
            foreach (var column in viewModel.Columns)
            {
                gridView.Columns.Add(column);
            }
        }

        private void QueryPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeFocus();
        }

        private void InitializeFocus()
        {
            viewModel.Items.OfType<TextBox>().FirstOrDefault()?.Focus();
        }

        private void CommandBinding_OnRefreshExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.Query();
        }

        private void CommandBinding_OnResetExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.Clear();
        }

        private void CommandBinding_OnExportExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.Export();
        }
    }
}
