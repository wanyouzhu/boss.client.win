namespace boss.client.win
{
    public static class ApplicationEvents
    {
        public delegate void StartMenuRequestedEvent();
        public static event StartMenuRequestedEvent StartMenuRequested;
        public static void OnStartMenuRequested()
        {
            StartMenuRequested?.Invoke();
        }

        public delegate void SearchBoxRequestedEvent();
        public static event SearchBoxRequestedEvent SearchBoxRequested;
        public static void OnSearchBoxRequested()
        {
            SearchBoxRequested?.Invoke();
        }

        public delegate void MenuItemSelectedEvent(MenuItem item, object parameter);
        public static event MenuItemSelectedEvent MenuItemSelected;
        public static void OnMenuItemSelected(MenuItem item, object parameter = null)
        {
            MenuItemSelected?.Invoke(item, parameter);
        }

        public delegate void CloseActivePageRequestedEvent();
        public static event CloseActivePageRequestedEvent CloseActivePageRequested;
        public static void OnCloseActivePageRequested()
        {
            CloseActivePageRequested?.Invoke();
        }
    }
}
