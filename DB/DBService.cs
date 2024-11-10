using DB.Models;

namespace DB
{
    public class DBService
    {
        private readonly BitcoinHistoryDbContext _appDbContext;
        public DBService(BitcoinHistoryDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public List<Record> GetRecords()
        {
            return _appDbContext.Records.ToList();
        }
        public List<Currency> GetCurrencies()
        {
            return _appDbContext.Currencies.ToList();
        }
        public int GetCurrencyIdByCode(string code)
        {
            return _appDbContext.Currencies.Single(d => d.Code == code).Id;
        }
        public void AddNewRecord(Record record, List<Rate> rates)
        {
            _appDbContext.Records.Add(record);
            _appDbContext.SaveChanges();
            foreach (var rate in rates)
            {
                rate.RecordId = record.Id;
                _appDbContext.Rates.Add(rate);
            }
            _appDbContext.SaveChanges();
        }
        public void RemoveRecord(int id)
        {
            var record = _appDbContext.Records.Single(r => r.Id == id);
            foreach (var rate in record.Rates)
            {
                _appDbContext.Rates.Remove(rate);
            }
            _appDbContext.Records.Remove(record);
            _appDbContext.SaveChanges();
        }
        public void UpdateNoteInDatabase(int recordId, string note)
        {
            var record = _appDbContext.Records.Single(d => d.Id == recordId);
            record.Note = note;
            _appDbContext.SaveChanges();
        }
    }
}
