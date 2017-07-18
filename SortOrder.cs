using System;
using System.ComponentModel;

namespace boss.client.win
{
    public class SortOrder
    {
        public string property { get; set; }
        public string direction { get; set; }

        public SortOrder(string property, string direction)
        {
            this.property = property;
            this.direction = direction;
        }

        public SortOrder(string property)
        {
            this.property = property;
        }

        public SortOrder()
        {
        }

        public ListSortDirection? GetSortDirection()
        {
            var isDesc = string.Compare(direction, "desc", StringComparison.InvariantCultureIgnoreCase) == 0;
            return isDesc ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        public void Toggle()
        {
            direction = string.Equals(direction, "ASC") ? "DESC" : "ASC";
        }
    }
}