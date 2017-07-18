using System.Net;
using RestSharp;

namespace boss.client.win
{
    public abstract class RestService
    {
        private readonly string serviceUrl;
        private readonly CookieContainer cookieContainer;

        protected RestService(string serviceUrl, CookieContainer cookieContainer)
        {
            this.serviceUrl = serviceUrl;
            this.cookieContainer = cookieContainer;
            rest = new RestTemplate(serviceUrl, cookieContainer);
            rest.AddDefaultHeader("X-Requested-With", "XMLHttpRequest");
            rest.AddDefaultHeader("Accept-Language", "zh-CN"); // TODO: i18n
            rest.UnauthenticatedHandler = HandleUnauthenticated;
        }

        private bool HandleUnauthenticated()
        {
            var loginClient = new RestTemplate(serviceUrl, cookieContainer);
            var request = new RestRequest("info", Method.GET);
            request.AddHeader("Accept", "*/*");
            loginClient.Exchange(request);
            return true;
        }

        protected RestTemplate rest
        {
            get;
        }

        protected void RequireNotNull(object result, string message)
        {
            if (result == null)
            {
                throw new ApplicationException(message);
            }
        }
    }
}