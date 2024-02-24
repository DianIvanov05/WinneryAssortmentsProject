namespace WineryAssortments.Data
{
    public class Promotion
    {
        public int Id { get; set; }
        public int WinesId { get; set; }
        public Wine Wines { get; set; }
        public int PromotionPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}