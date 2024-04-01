using System.ComponentModel;

namespace WineryAssortments.Data
{
    public class WineType
    {
        public int Id { get; set; }
        [DisplayName("Име на вида вино")]
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        [DisplayName("Дата на промяна")]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}