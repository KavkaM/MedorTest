namespace Web.Models
{
    public class SavedDataViewModel
    {
        public List<ViewModelSavedRow> SavedRows { get; set; }
        public List<ViewModelCurrency> Currencies { get; set; }
    }

    public class ViewModelSavedRow
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, decimal> Rates { get; set; }
        public string Note { get; set; }
        public bool Selected { get; set; }
    }

    public class ViewModelCurrency
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
    }
}
