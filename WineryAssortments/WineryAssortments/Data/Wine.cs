using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WineryAssortments.Data
{
    public class Wine
    {
        public int Id { get; set; }
        [DisplayName("Име на виното")]
        public string Name { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Снимка")]
        public string ImageUrl { get; set; }
        [DisplayName("Дата на промяна")]
        public DateTime DateModified { get; set; } = DateTime.Now;
        public int WineCattegoriesId { get; set; }
        [DisplayName("Категория на виното")]
        public WineCattegory WineCattegories { get; set; }    
        public int WineTypesId { get; set; }
        [DisplayName("Вид на виното")]
        public WineType WineTypes { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
