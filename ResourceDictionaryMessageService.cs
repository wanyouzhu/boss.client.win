using System.Windows;

namespace boss.client.win
{
    [Service]
    public class ResourceDictionaryMessageService : IMessageService
    {
        public string FormatMessage(string messageId, params object[] parameters)
        {
            var messageTemplate = Application.Current.TryFindResource(messageId) as string;
            return messageTemplate == null ? messageId : string.Format(messageTemplate, parameters);
        }
    }
}