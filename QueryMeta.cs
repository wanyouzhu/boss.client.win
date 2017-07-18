using System.Collections.Generic;
using System.Linq;

namespace boss.client.win
{
    public class QueryMeta
    {
        public string code { get; set; }
        public string name { get; set; }
        public Parameter[] parameters { get; set; }
        public Field[] fields { get; set; }
        public IdentityField[] identityFields { get; set; }

        public IEnumerable<Field> GetDisplayFields()
        {
            var result = new[] { new Field { name = "rowNumber", type = FieldTypes.Number, description = "序号" } }; // TODO: i18n
            return result.Concat(fields?.Where(x => !x.name.StartsWith("_")).ToList() ?? new List<Field>()).ToList();
        }

        public IEnumerable<IdentityField> GetIdentityFields()
        {
            return identityFields ?? new IdentityField[0];
        }

        public IEnumerable<IdentityField> GetPrimaryIdentityFields()
        {
            return GetIdentityFields().Where(x => x.primary).ToList();
        }
    }

    public class Parameter
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