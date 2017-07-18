using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace boss.client.win
{
    public class RestTemplate
    {
        private readonly string serviceUrl;
        private readonly RestClient rest;
        private readonly RestJsonSerializer jsonSerializer = new RestJsonSerializer();

        public RestTemplate(CookieContainer cookieContainer)
        {
            rest = new RestClient() { CookieContainer = cookieContainer };
        }

        public RestTemplate(string serviceUrl, CookieContainer cookieContainer)
        {
            this.serviceUrl = serviceUrl;
            rest = new RestClient(serviceUrl) { CookieContainer = cookieContainer };
        }

        public bool FollowRedirects
        {
            get { return rest.FollowRedirects; }
            set { rest.FollowRedirects = value; }
        }

        public Func<bool> UnauthenticatedHandler
        {
            get; set;
        }

        public IAuthenticator Authenticator
        {
            get { return rest.Authenticator; }
            set { rest.Authenticator = value; }
        }

        public Uri BaseUrl
        {
            get { return rest.BaseUrl; }
            set { rest.BaseUrl = value; }
        }

        public void AddDefaultHeader(string header, string value)
        {
            rest.AddDefaultHeader(header, value);
        }

        public T PostForObject<T>(string url) where T : new()
        {
            return PostForObject<T>(url, null);
        }

        public T PostForObject<T>(string url, object body) where T : new()
        {
            var request = new RestRequest(url, Method.POST) { JsonSerializer = jsonSerializer };
            request.AddJsonBody(body);
            return JsonConvert.DeserializeObject<T>(Exchange(request).Content);
        }

        public T GetForObject<T>(string url) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(Exchange(new RestRequest(url, Method.GET)).Content);
        }

        public T PutForObject<T>(string url, object body) where T : new()
        {
            var request = new RestRequest(url, Method.PUT) { JsonSerializer = jsonSerializer };
            request.AddJsonBody(body);
            return JsonConvert.DeserializeObject<T>(Exchange(request).Content);
        }

        public T GetForObject<T>(string url, IDictionary<string, object> parameters) where T : new()
        {
            var request = new RestRequest(url, Method.GET);
            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }
            return JsonConvert.DeserializeObject<T>(Exchange(request).Content);
        }

        public IRestResponse GetForEntity(string url, IDictionary<string, object> parameters)
        {
            var request = new RestRequest(url, Method.GET);
            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }
            return Exchange(request);
        }

        public T Exchange<T>(RestRequest request)
        {
            return JsonConvert.DeserializeObject<T>(Exchange(request).Content);
        }

        public IRestResponse Exchange(RestRequest request)
        {
            try
            {
                return TryExchange(request);
            }
            catch (UnauthorizedAccessException)
            {
                if (UnauthenticatedHandler == null) throw;
                if (!UnauthenticatedHandler()) throw;
                return TryExchange(request);
            }
        }

        private IRestResponse TryExchange(RestRequest request)
        {
            var response = rest.Execute(request);
            CheckHttpStatusCode(response);
            return response;
        }

        private void CheckHttpStatusCode(IRestResponse response)
        {
            if (response.ErrorException != null) throw response.ErrorException;
            if (response.StatusCode < HttpStatusCode.BadRequest) return;
            if (response.StatusCode.Equals(HttpStatusCode.Unauthorized)) throw new UnauthorizedAccessException();
            throw new RestException("error.error-from-server", response.Content);
        }
    }
}