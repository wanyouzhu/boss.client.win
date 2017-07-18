namespace boss.client.win
{
    public class RestException : ApplicationException
    {
        public RestException(string messageCode, string content) : base(messageCode, content)
        {
        }
    }
}