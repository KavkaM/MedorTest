using DB;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.CoinDesk;
using Web.Services;

namespace Web.Controllers
{
    public class LiveDataController : Controller
    {
        private readonly ILogger<LiveDataController> _logger;
        private readonly CoinDeskService _coinDeskService;
        private readonly CNBService _cnbService;
        private readonly DBService _dbService;
        public LiveDataController(ILogger<LiveDataController> logger, CoinDeskService coinDeskService, CNBService cnbService, DBService dbService)
        {
            _logger = logger;
            _cnbService = cnbService;
            _coinDeskService = coinDeskService;
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            var coinDeskResponse = _coinDeskService.GetBTCPrices();
            if (coinDeskResponse == null) return RedirectToAction("Index", "Error");
            var btcPriceInCZK = GetCZKRate(coinDeskResponse);
            var liveDataViewModel = new LiveDataViewModel()
            {
                Data = new Dictionary<string, decimal>()
            };

            foreach (var currency in coinDeskResponse.Currencies)
            {
                liveDataViewModel.Data.Add(currency.Key, currency.Value.RateFloat);
            }
            liveDataViewModel.Data.Add("CZK", btcPriceInCZK);
            return View(liveDataViewModel);
        }
        public IActionResult SaveData()
        {
            var coinDeskResponse = _coinDeskService.GetBTCPrices();
            List<Rate> rates = new List<Rate>();

            foreach (var currency in coinDeskResponse.Currencies)
            {                
                rates.Add(PrepareRateForDB(currency.Key, currency.Value.RateFloat));
            }
            rates.Add(PrepareRateForDB("CZK", GetCZKRate(coinDeskResponse)));

            var record = new Record()
            {
                UpdatedAt = coinDeskResponse.Time.UpdatedISO
            };
            _dbService.AddNewRecord(record, rates);
            return RedirectToAction("Index");
        }
        private Rate PrepareRateForDB(string currencyCode, decimal rate)
        {
            var currencyId = _dbService.GetCurrencyIdByCode(currencyCode);
            var rateInDB = new Rate()
            {
                CurrencyId = currencyId,
                Value = rate
            };
            return rateInDB;
        }
        private decimal GetCZKRate(CoinDeskResponse coinDeskResponse)
        {            
            return _cnbService.ExchangeEURtoCZK(coinDeskResponse.Currencies["EUR"].RateFloat).Result;
        }
    }
}
