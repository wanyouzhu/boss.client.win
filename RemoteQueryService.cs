using System.Collections.Generic;
using boss.client.win.Properties;

namespace boss.client.win
{
    [Service]
    public class RemoteQueryService : RestService, IQueryService
    {
        public RemoteQueryService(DefaultCookieContainer cookieContainer) : base(Settings.Default.ReportingServiceHost, cookieContainer)
        {
        }

        public QueryMeta GetQueryMeta(string queryCode)
        {
            return rest.GetForObject<QueryMeta>($"query/{queryCode}/meta", RestClientExtension.Parameters());
        }

        public QueryResult ExecuteQuery(Query query)
        {
            var result = rest.PostForObject<QueryResult>($"query/{query.code}/execute", query);
            RequireNotNull(result, "msg_error_result_of_query_from_server_should_not_be_null");
            return result;
        }

        public SingleResult ExecuteQueryAsSingle(string code, IDictionary<string, object> arguments)
        {
            var result = rest.PostForObject<SingleResult>($"query/{code}/execute/single", arguments);
            RequireNotNull(result, "msg_error_result_of_query_from_server_should_not_be_null");
            return result;
        }
    }
}