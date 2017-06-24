using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace boss.client.win
{
    public class StartButton : Button
    {
        public StartButton()
        {
            Click += StartButton_Click;
        }

        private static void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationEvents.OnStartMenuRequested();
        }
    }
}
