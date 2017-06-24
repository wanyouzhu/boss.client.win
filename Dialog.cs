using System;
using System.Windows;
using System.Windows.Threading;
using Autofac;

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

        public static void Error(Exception e)
        {
            MessageBox.Show(GetOwner(), GetErrorMessage(e), GetCaption(), MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private static string GetErrorMessage(Exception e)
        {
            var exception = e as ApplicationException;
            return exception != null ? exception.Format(ApplicationContext.ComponentContext.Resolve<IMessageService>()) : e.ToString();
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