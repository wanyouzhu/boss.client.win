using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Autofac;
using boss.client.win.common.controls;

namespace boss.client.win
{
    public abstract class AbstractQueryViewModel : DependencyObject
    {
        private readonly string profileKey;
        private readonly IQueryService queryService;
        private readonly List<SortOrder> sort = new List<SortOrder>();
        private QueryMeta queryMeta = new QueryMeta();

        protected AbstractQueryViewModel(string queryCode, string profileKey)
        {
            QueryCode = queryCode;
            this.profileKey = profileKey;
            queryService = ApplicationContext.ComponentContext.Resolve<IQueryService>();
            Columns = new DataGridColumn[0];
        }

        public static readonly DependencyProperty QueryCodeProperty = DependencyProperty.Register(
            "QueryCode", typeof(string), typeof(AbstractQueryViewModel), new PropertyMetadata(default(string)));

        public string QueryCode
        {
            get { return (string) GetValue(QueryCodeProperty); }
            private set { SetValue(QueryCodeProperty, value); }
        }

        public static readonly DependencyProperty QueryNameProperty = DependencyProperty.Register(
            "QueryName", typeof(string), typeof(AbstractQueryViewModel), new PropertyMetadata(default(string)));

        public string QueryName
        {
            get { return (string) GetValue(QueryNameProperty); }
            set { SetValue(QueryNameProperty, value); }
        }

        public static readonly DependencyProperty QueryResultProperty = DependencyProperty.Register(
            "QueryResult", typeof(QueryResult), typeof(AbstractQueryViewModel), new PropertyMetadata(default(QueryResult)));

        public QueryResult QueryResult
        {
            get { return (QueryResult)GetValue(QueryResultProperty); }
            set { SetValue(QueryResultProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register(
            "PageSize", typeof(int), typeof(AbstractQueryViewModel), new PropertyMetadata(30));

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty NumberOfPagesProperty = DependencyProperty.Register(
            "NumberOfPages", typeof(int), typeof(AbstractQueryViewModel), new PropertyMetadata(default(int)));

        public int NumberOfPages
        {
            get { return (int)GetValue(NumberOfPagesProperty); }
            set { SetValue(NumberOfPagesProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
            "CurrentPage", typeof(int), typeof(AbstractQueryViewModel), new PropertyMetadata(default(int)));

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable<FrameworkElement>), typeof(AbstractQueryViewModel), new PropertyMetadata(default(IEnumerable<FrameworkElement>)));

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

        private FrameworkElement[] CreateParameter(Parameter parameter)
        {
            return new[] { CreateParameterHeader(parameter), CreateParameterContent(parameter) };
        }

        protected abstract FrameworkElement CreateParameterContent(Parameter parameter);

        protected abstract FrameworkElement CreateParameterHeader(Parameter parameter);

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
            HandleProfileLoaded();
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
            var column = new DataGridTextColumn() { Header = x.description, Binding = binding };
            if (x.type.Equals(FieldTypes.DateTime))
            {
                binding.Converter = new DateTimeConvierter();
            }
            else if (x.type.Equals(FieldTypes.Number))
            {
                column.ElementStyle = (Style)Application.Current.FindResource("DataGridRightAlignedCellStyle");
                column.HeaderStyle = (Style)Application.Current.FindResource("DataGridRightAlignedColumnHeaderStyle");
            }
            return column;
        }

        private IEnumerable<Parameter> GetMetaQueryParameters()
        {
            return queryMeta?.parameters;
        }

        public void Save()
        {
            // TODO: implement it
        }

        public void GotoFirstPage()
        {
            // TODO: implement it
        }

        public void GotoLastPage()
        {
            // TODO: implement it
        }

        public void GotoPreviousSection()
        {
            // TODO: implement it
        }

        public void GotoNextSection()
        {
            // TODO: implement it
        }

        public void GotoPreviousPage()
        {
            // TODO: implement it
        }

        public void GotoNextPage()
        {
            // TODO: implement it
        }

        public void SortField(string property, bool multiSort)
        {
            var sortOrder = sort.FirstOrDefault(x => string.Equals(x.property, property)) ?? new SortOrder(property);
            sortOrder.Toggle();
            RemoveStaleSortOrders(multiSort, sortOrder);
            sort.Add(sortOrder);
            Query();
        }

        private void RemoveStaleSortOrders(bool multiSort, SortOrder sortOrder)
        {
            if (multiSort)
            {
                sort.Remove(sortOrder);
            }
            else
            {
                sort.Clear();
            }
        }

        public void Query()
        {
            Query(CurrentPage);
        }

        private void Query(int pageIndex)
        {
            var pagination = GetQueryPagination(pageIndex);
            var result = queryService.ExecuteQuery(new Query(QueryCode, GetQueryArguments(), pagination, sort));
            result.SetPagination(pagination);
            result.ResolveRowNumbers();
            QueryResult = result;
        }

        protected abstract IDictionary<string, object> GetQueryArguments();

        protected IEnumerable<IInputBox> GetInputBoxes()
        {
            return Items.OfType<IInputBox>();
        }

        private Pagination GetQueryPagination(int pageIndex)
        {
            return new Pagination(pageIndex, PageSize);
        }

        public void Clear()
        {
            foreach (var box in GetInputBoxes())
            {
                box.ClearValue();
            }
        }

        public void Export()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void HandleProfileLoaded()
        {
        }
    }
}