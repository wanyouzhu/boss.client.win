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
    }
}
