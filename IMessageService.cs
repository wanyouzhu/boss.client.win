namespace boss.client.win
{
    public interface IMessageService
    {
        string FormatMessage(string messageId, params object[] parameters);
    }
}