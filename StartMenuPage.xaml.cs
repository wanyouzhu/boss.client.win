using System.Windows;
using System.Windows.Input;

namespace boss.client.win
{
    public partial class StartMenuPage
    {
        public StartMenuPage()
        {
            InitializeComponent();
        }

        private void StartMenuPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }

        private void StartMenuPage_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown && e.Key == Key.Escape)
            {
                ApplicationEvents.OnStartMenuRequested();
            }
        }
    }
}
