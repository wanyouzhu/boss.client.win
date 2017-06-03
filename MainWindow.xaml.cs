using System.ComponentModel;

namespace boss.client.win
{
    public partial class MainWindow
    {
        private readonly StartMenuPage startMenu = new StartMenuPage();

        public MainWindow()
        {
            InitializeComponent();
            SetContent(new FramePage());
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
        }

        private void HideStartMenu()
        {
            SetPopupContent(null);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Dialog.ConfirmYesNo(new DialogParameters { Message = "您确定要退出系统吗？", OnNo = () => e.Cancel = true});
        }
    }
}
