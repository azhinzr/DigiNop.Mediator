namespace Contracts.Digikala
{
    public class Pager
    {
        public int page { get; set; }
        public int item_per_page { get; set; }
        public int total_page { get; set; }
        public int total_rows { get; set; }
    }
}