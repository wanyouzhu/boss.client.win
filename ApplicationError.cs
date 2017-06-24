namespace boss.client.win
{
    public class ApplicationError : ApplicationException
    {
        public ApplicationError(string messageId, params object[] parameters) : base(messageId, parameters)
        {
        }

        public override string Format(IMessageService messageService)
        {
            var prefix = messageService.FormatMessage("message.system-error");
            return prefix + "\n" + base.Format(messageService);
        }
    }
}