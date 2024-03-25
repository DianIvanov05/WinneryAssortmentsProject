using System.ComponentModel;

namespace WineryAssortments.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int WinesId { get; set; }

        [DisplayName("Име на виното")]
        public Wine Wines { get; set; }
        public string CustomersId { get; set; }

        [DisplayName("Поръчано от")]
        public Customer Customers { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [DisplayName("Дата на поръчка")]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}