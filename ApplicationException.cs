using System;

namespace boss.client.win
{
    public class ApplicationException : Exception
    {
        private readonly object[] parameters;

        public ApplicationException(string messageId, params object[] parameters) : base(messageId)
        {
            this.parameters = parameters;
        }

        public virtual string Format(IMessageService messageService)
        {
            return messageService.FormatMessage(Message, parameters);
        }
    }
}