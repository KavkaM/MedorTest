using Newtonsoft.Json;
using Web.Models.CoinDesk;

namespace Web.Services
{
    public class CoinDeskService
    {
        private readonly HttpClient _httpClient;
        private System.Timers.Timer _timer;
        private CoinDeskResponse _lastCoinDeskResponse;
        private readonly ILogger<CoinDeskService> _logger;
        private double _timerInterval = 30000;

        public CoinDeskService(ILogger<CoinDeskService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            InitializeTimer();
            UpdateCurrentBTCPrices().Wait();
            
        }
        public CoinDeskResponse GetBTCPrices()
        {
            return _lastCoinDeskResponse;
        }
        public double GetTimerInterval()
        {
            return _timerInterval;
        }
        private async Task<CoinDeskResponse> GetCurrentBTCPrices()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                var data = JsonConvert.DeserializeObject<CoinDeskResponse>(response);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Nepodařilo se spojit s CoinDesk.");
                return null;
            }
        }
        private async Task UpdateCurrentBTCPrices()
        {
            _lastCoinDeskResponse = await GetCurrentBTCPrices();
        }
        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = _timerInterval;
            _timer.Elapsed += async (sender, args) => await UpdateCurrentBTCPrices();
            _timer.Start();
        }
    }
}
