namespace boss.client.win
{
    public class Pagination
    {
        public Pagination(int page, int size)
        {
            this.page = page;
            this.size = size;
        }

        public int page
        {
            get;
        }

        public int size
        {
            get;
        }
    }
}