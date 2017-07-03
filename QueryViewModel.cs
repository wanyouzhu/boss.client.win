using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Autofac;

namespace boss.client.win
{
    public class QueryViewModel : DependencyObject
    {
        private readonly IQueryService queryService;
        private QueryMeta queryMeta = new QueryMeta();
        private readonly string labelTail;

        public QueryViewModel()
        {
            labelTail = Application.Current.TryFindResource("message.label.tail") as string ?? string.Empty;
            queryService = ApplicationContext.ComponentContext.Resolve<IQueryService>();
            Columns = new DataGridColumn[0];
        }

        public static readonly DependencyProperty QueryCodeProperty = DependencyProperty.Register(
            "QueryCode", typeof(string), typeof(QueryViewModel), new PropertyMetadata(default(string)));

        public string QueryCode
        {
            get { return (string) GetValue(QueryCodeProperty); }
            set { SetValue(QueryCodeProperty, value); }
        }

        public static readonly DependencyProperty QueryNameProperty = DependencyProperty.Register(
            "QueryName", typeof(string), typeof(QueryViewModel), new PropertyMetadata(default(string)));

        public string QueryName
        {
            get { return (string) GetValue(QueryNameProperty); }
            set { SetValue(QueryNameProperty, value); }
        }

        public static readonly DependencyProperty QueryResultProperty = DependencyProperty.Register(
            "QueryResult", typeof(QueryResult), typeof(QueryViewModel), new PropertyMetadata(default(QueryResult)));

        public QueryResult QueryResult
        {
            get { return (QueryResult)GetValue(QueryResultProperty); }
            set { SetValue(QueryResultProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register(
            "PageSize", typeof(int), typeof(QueryViewModel), new PropertyMetadata(default(int)));

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty NumberOfPagesProperty = DependencyProperty.Register(
            "NumberOfPages", typeof(int), typeof(QueryViewModel), new PropertyMetadata(default(int)));

        public int NumberOfPages
        {
            get { return (int)GetValue(NumberOfPagesProperty); }
            set { SetValue(NumberOfPagesProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
            "CurrentPage", typeof(int), typeof(QueryViewModel), new PropertyMetadata(default(int)));

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable<FrameworkElement>), typeof(QueryViewModel), new PropertyMetadata(default(IEnumerable<FrameworkElement>)));

        public IEnumerable<FrameworkElement> Items
        {
            get { return (IEnumerable<FrameworkElement>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }

        private IEnumerable<FrameworkElement> LoadParameterUiItems()
        {
            var visiableParameters = GetMetaQueryParameters()?.Where(x => !x.name.StartsWith("_")).ToList();
            return visiableParameters?.Select(CreateParameter).SelectMany(x => x).ToArray();
        }

        private FrameworkElement[] CreateParameter(QueryParameter parameter)
        {
            return new[] { CreateParameterHeader(parameter), CreateParameterContent(parameter) };
        }

        private FrameworkElement CreateParameterContent(QueryParameter parameter)
        {
            return InputBox.Of(parameter);
        }

        private FrameworkElement CreateParameterHeader(QueryParameter parameter)
        {
            return new Label() { Content = parameter.description + labelTail };
        }

        public IEnumerable<DataGridColumn> Columns
        {
            get; private set;
        }

        public void Load()
        {
            queryMeta = queryService.GetQueryMeta(QueryCode);
            QueryName = queryMeta.name;
            Columns = GenerateColumns();
            Items = LoadParameterUiItems();
        }

        private IEnumerable<DataGridBoundColumn> GenerateColumns()
        {
            if (queryMeta == null) return new DataGridBoundColumn[0];
            var columns = queryMeta.GetDisplayFields().Select(CreateDataGridColumn).ToList();
            FrozeRowNumberColumn(columns);
            ApplyProfile(columns);
            return columns.OrderBy(x => x.DisplayIndex).ToList();
        }

        private void ApplyProfile(List<DataGridTextColumn> columns)
        {
        }

        private static void FrozeRowNumberColumn(List<DataGridTextColumn> columns)
        {
            var rowNumberColumn = columns.FirstOrDefault(x => x.SortMemberPath.Equals("rowNumber"));
            if (rowNumberColumn == null) return;
            rowNumberColumn.CanUserReorder = false;
            rowNumberColumn.CanUserSort = false;
        }

        private static DataGridTextColumn CreateDataGridColumn(Field x)
        {
            var binding = new Binding(x.name) { Mode = BindingMode.OneWay };
            if (x.type.Equals(ParameterTypes.DateTime))
            {
                binding.Converter = new UnixEpochConvierter();
            }
            return new DataGridTextColumn() { Header = x.description, Binding = binding };
        }

        private IReadOnlyList<QueryParameter> GetMetaQueryParameters()
        {
            return queryMeta?.parameters;
        }

        public void Query()
        {
            var result = new QueryResult
            {
                data = new object[]
                {
                    new {rowNumber = 1, customerCode = "640101001", customerName = "640101001xxx", productCode = "030301", productName = "五菱TIS网页版"},
                    new {rowNumber = 2}
                }
            };
            QueryResult = result;
        }
    }
}