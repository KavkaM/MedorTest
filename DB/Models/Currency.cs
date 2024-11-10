namespace DB.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
