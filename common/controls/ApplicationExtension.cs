using System.Linq;
using System.Windows;

namespace boss.client.win.common.controls
{
    public static class ApplicationExtension
    {
        public static Window GetActiveWindow(this Application application)
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
    }
}