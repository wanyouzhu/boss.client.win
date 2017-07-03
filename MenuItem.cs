using System.Collections.Generic;

namespace boss.client.win
{
    public class MenuItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<MenuItem> Items { get; set; }
        public string Icon => string.Equals(Type, "01") ? "\uf114" : (string.Equals(Type, "02") ? "\uf040" : (string.Equals(Type, "03") ? "\uf0f6" : (string.Equals(Type, "04") ? "\uf080" : "\uf05e")));
    }
}