﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autofac;
using static boss.client.win.ApplicationContext;

namespace boss.client.win
{
    public partial class MainView
    {
        private readonly ObservableCollection<Page> pages = new ObservableCollection<Page>();
        private readonly IApplicationService applicationService;

        public MainView()
        {
            applicationService = ComponentContext.Resolve<IApplicationService>();
            Pages = new ObservableCollection<PageTabItem>();
            DataContext = this;
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            ApplicationEvents.MenuItemSelected += ApplicationEvents_MenuItemSelected;
            ApplicationEvents.CloseActivePageRequested += ApplicationEvents_CloseActivePageRequested;
        }

        private void ApplicationEvents_CloseActivePageRequested()
        {
            if (ActiveItem == null) return;
            Pages.Remove(ActiveItem);
        }

        private void ApplicationEvents_MenuItemSelected(MenuItem item, object parameter)
        {
            ActiveItem = GetOrAddPage(item, parameter);
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplicationEvents.OnMenuItemSelected(applicationService.GetWorkbenchMenuItem());
        }

        public ObservableCollection<PageTabItem> Pages
        {
            get;
        }

        private PageTabItem GetOrAddPage(MenuItem menuItem, object parameter)
        {
            var existingPage = Pages.FirstOrDefault(x => x.IsBelongTo(menuItem, parameter));
            if (existingPage != null) return existingPage;
            var item = CreateNewItem(menuItem, parameter);
            Pages.Add(item);
            return item;
        }

        private static PageTabItem CreateNewItem(MenuItem menuItem, object parameter)
        {
            var page = CreatePage(menuItem, parameter);
            page.Code = menuItem.Code;
            page.Title = menuItem.Name;
            page.Icon = menuItem.Icon;
            var result = new PageTabItem(menuItem, parameter) { Content = page };
            result.CommandBindings.AddRange(page.CommandBindings);
            result.InputBindings.AddRange(page.InputBindings);
            return result;
        }

        private static Page CreatePage(MenuItem menuItem, object parameter)
        {
            switch (menuItem.Type)
            {
                case "02": return CreateFunctionalPage(menuItem, parameter);
                case "03": return CreateQueryPage(menuItem, parameter);
                case "04": return CreateChartPage(menuItem, parameter);
                default: throw new ApplicationError("message.unknown-menu-type", menuItem.Type);
            }
        }

        private static Page CreateFunctionalPage(MenuItem menuItem, object parameter)
        {
            return ComponentContext.ResolvePage(menuItem.Code, parameter);
        }

        private static Page CreateQueryPage(MenuItem menuItem, object parameter)
        {
            return ComponentContext.ResolvePage("__QUERY__", new { queryCode = menuItem.Code, parameter });
        }

        private static Page CreateChartPage(MenuItem menuItem, object parameter)
        {
            throw new NotImplementedException();
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
