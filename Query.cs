using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace boss.client.win
{
    public class Query
    {
        public Query(string code, IDictionary<string, object> arguments) : this(code, arguments, null)
        {
        }

        public Query(string code, IDictionary<string, object> arguments, Pagination pagination) : this(code, arguments, pagination, new SortOrder[0])
        {
        }

        public Query(string code, IDictionary<string, object> arguments, Pagination pagination, ICollection<SortOrder> sort)
        {
            this.code = code;
            this.arguments = arguments;
            this.pagination = pagination;
            this.sort = sort.ToArray();
        }

        public string code
        {
            get;
        }

        public IDictionary<string, object> arguments
        {
            get;
        }

        public Pagination pagination
        {
            get;
        }

        public SortOrder[] sort
        {
            get;
        }
    }
}