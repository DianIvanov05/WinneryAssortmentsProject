namespace WineryAssortments.Data
{
    public class WineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}