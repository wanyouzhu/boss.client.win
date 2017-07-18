using System.Collections.Generic;

namespace boss.client.win
{
    public interface IQueryService
    {
        QueryMeta GetQueryMeta(string code);
        QueryResult ExecuteQuery(Query query);
        SingleResult ExecuteQueryAsSingle(string code, IDictionary<string, object> arguments);
    }
}