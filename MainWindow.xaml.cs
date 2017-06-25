using System.ComponentModel;

namespace boss.client.win
{
    public partial class MainWindow
    {
        private readonly StartMenuPage startMenu = new StartMenuPage();

        public MainWindow()
        {
            InitializeComponent();
            SetContent(new MainView());
            InitializeEventHandler();
        }

        private void InitializeEventHandler()
        {
            ApplicationEvents.StartMenuRequested += ApplicationEventsStartMenuRequested;
            ApplicationEvents.MenuItemSelected += ApplicationEvents_MenuItemSelected;
        }

        private void ApplicationEvents_MenuItemSelected(MenuItem item, object parameter)
        {
            if (!IsStartMenuPoppedUp()) return;
            ApplicationEvents.OnStartMenuRequested();
        }

        private void ApplicationEventsStartMenuRequested()
        {
            if (IsStartMenuPoppedUp())
            {
                HideStartMenu();
            }
            else
            {
                PopupStartMenu();
            }
        }

        private bool IsStartMenuPoppedUp()
        {
            return ReferenceEquals(PopupLayer.Content, startMenu);
        }

        private void SetContent(object content)
        {
            ContentLayer.Content = content;
        }

        private void SetPopupContent(object content)
        {
            PopupLayer.Content = content;
        }

        private void PopupStartMenu()
        {
            SetPopupContent(startMenu);
            ContentLayer.IsEnabled = false;
        }

        private void HideStartMenu()
        {
            ContentLayer.IsEnabled = true;
            SetPopupContent(null);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Dialog.ConfirmYesNo(new DialogParameters { Message = "您确定要退出系统吗？", OnNo = () => e.Cancel = true});
        }
    }
}
