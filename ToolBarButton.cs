using System.Windows;
using System.Windows.Controls;

namespace boss.client.win
{
    public class ToolBarButton : Button
    {
        public static readonly DependencyProperty IconTextProperty = DependencyProperty.Register(
            "IconText", typeof(string), typeof(ToolBarButton), new PropertyMetadata(default(string)));

        public string IconText
        {
            get { return (string) GetValue(IconTextProperty); }
            set { SetValue(IconTextProperty, value); }
        }
    }
}