namespace DB.Models
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Note { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
