namespace WinneryAssortments.Data
{
    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
        public int WineCattegoriesId { get; set; }
        public WineCattegory WineCattegories { get; set; }
        public int WineTypesId { get; set; }
        public WineType WineTypes { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
