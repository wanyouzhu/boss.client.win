using System.Collections.Generic;

namespace boss.client.win
{
    public class MenuItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<MenuItem> Items { get; set; }
    }
}