using System.Windows.Threading;
using boss.client.win.common.controls;

namespace boss.client.win
{
    public partial class App
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Dialog.Error(e.Exception);
            e.Handled = true;
        }
    }
}
