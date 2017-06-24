using System.Windows.Threading;

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
