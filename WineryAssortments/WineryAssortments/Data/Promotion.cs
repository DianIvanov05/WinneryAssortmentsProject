using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WineryAssortments.Data
{
    public class Promotion
    {
        public int Id { get; set; }
        public int WinesId { get; set; }
        [DisplayName("Промоционално вино")]
        public Wine Wines { get; set; }
        [DisplayName("Промоция")]
        public int PromotionPercent { get; set; }
        [DisplayName("Начална дата")]     
        public DateTime StartDate { get; set; }
        [DisplayName("Крайна дата")]
        public DateTime EndDate { get; set; }
        [DisplayName("Дата на промяна")]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}