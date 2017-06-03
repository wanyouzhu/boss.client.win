using System;
using System.Windows;

namespace boss.client.win
{
    public class Dialog
    {
        public static void ConfirmYesNo(DialogParameters parameters)
        {
            if (MessageBox.Show(GetOwner(), parameters.Message, GetCaption(), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes)
            {
                parameters.OnNo?.Invoke();
            }
            else
            {
                parameters.OnYes?.Invoke();
            }
        }

        private static string GetCaption()
        {
            return GetOwner().TryFindResource("ApplicationName") as string;
        }

        private static Window GetOwner()
        {
            return Application.Current?.MainWindow;
        }
    }

    public class DialogParameters
    {
        public string Message { get; set; }
        public Action OnYes { get; set; }
        public Action OnNo { get; set; }
        public Action OnCancel { get; set; }
    }
}