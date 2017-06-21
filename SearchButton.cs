using System.Windows;
using System.Windows.Controls;

namespace boss.client.win
{
    public class SearchButton : Button
    {
        public SearchButton()
        {
            Click += SearchButton_Click;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationEvents.OnSearchBoxRequested();
        }
    }
}