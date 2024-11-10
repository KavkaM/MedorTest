namespace DB.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int RecordId { get; set; }
        public virtual Record Record { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
