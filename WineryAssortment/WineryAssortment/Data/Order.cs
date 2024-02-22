namespace WineryAssortment.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int WinesId { get; set; }
        public Wine Wines { get; set; }
        public string CustomersId { get; set; }
        public Customer Customers { get; set; }
        public int Quantity { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}