using System;

namespace boss.client.win
{
    public class QueryResult
    {
        public long totalNumbers { get; set; }
        public object[] data { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
        public int size { get; set; }

        public void SetPagination(Pagination pagination)
        {
            size = pagination.size;
            totalPages = (int)(totalNumbers / size + (totalNumbers % size == 0 ? 0 : 1));
            page = pagination.page < int.MaxValue ? Math.Min(pagination.page + 1, totalPages) : totalPages;
        }
    }
}