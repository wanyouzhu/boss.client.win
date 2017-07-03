using System.Collections.Generic;
using System.Linq;

namespace boss.client.win
{
    public class QueryMeta
    {
        public string code { get; set; }
        public string name { get; set; }
        public QueryParameter[] parameters { get; set; }
        public Field[] fields { get; set; }

        public IReadOnlyList<Field> GetDisplayFields()
        {
            return fields.Where(x => !x.name.StartsWith("_")).ToList();
        }
    }

    public class QueryParameter
    {
        public string name { get; set; }
        public string type { get; set; }
        public string subQueryCode { get; set; }
        public string description { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string description { get; set; }
    }
}