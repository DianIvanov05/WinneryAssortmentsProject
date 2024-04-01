using System.ComponentModel;

namespace WineryAssortments.Data
{
    public class WineCattegory
    {
        public int Id { get; set; }
        [DisplayName("Име на категорията")]
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        [DisplayName("Дата на промяна")]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}