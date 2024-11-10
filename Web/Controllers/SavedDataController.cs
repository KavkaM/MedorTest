using DB;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SavedDataController : Controller
    {
        private readonly ILogger<SavedDataController> _logger;
        private readonly DBService _dbService;

        public SavedDataController(ILogger<SavedDataController> logger, DBService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            var savedDataViewModel = new SavedDataViewModel()
            {
                Currencies = new List<ViewModelCurrency>(),
                SavedRows = new List<ViewModelSavedRow>()
            };
            foreach (var DBCurrency in _dbService.GetCurrencies())
            {
                var viewModelCurrency = new ViewModelCurrency()
                {
                    Id = DBCurrency.Id,
                    CurrencyCode = DBCurrency.Code
                };
                savedDataViewModel.Currencies.Add(viewModelCurrency);
            }
            foreach (var DBRecord in _dbService.GetRecords())
            {
                var viewModelSavedRow = new ViewModelSavedRow
                {
                    Id = DBRecord.Id,
                    Note = DBRecord.Note,
                    Date = DBRecord.UpdatedAt,
                    Rates = new Dictionary<int, decimal>()
                };
                foreach (var DBRate in DBRecord.Rates.ToList())
                {
                    viewModelSavedRow.Rates.Add(DBRate.CurrencyId, DBRate.Value);
                }

                savedDataViewModel.SavedRows.Add(viewModelSavedRow);
            }

            return View(savedDataViewModel);
        }
        public IActionResult SaveChanges(SavedDataViewModel savedDataViewModel)
        {
            foreach (var row in savedDataViewModel.SavedRows)
            {
                _dbService.UpdateNoteInDatabase(row.Id, row.Note);                
            }
            foreach (var id in savedDataViewModel.SavedRows.Where(d => d.Selected == true).Select(d=>d.Id))
            {
                _dbService.RemoveRecord(id);
            }
            return RedirectToAction("Index");
        }
    }
}
