using System.Globalization;

namespace Web.Services
{
    public class CNBService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CNBService> _logger;

        public CNBService(ILogger<CNBService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();            
        }
        private async Task<decimal> GetCZKExchangeRate()
        {
            try 
            {
                var response = await _httpClient.GetAsync("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt");

                var content = await response.Content.ReadAsStringAsync();
                var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines.Skip(1))
                {
                    var fields = line.Split('|');
                    if (fields[3] == "EUR")
                        return decimal.Parse(fields[4].Replace(',', '.'), CultureInfo.InvariantCulture);
                }
                throw new Exception("Nebyl nalezen řádek s EUR v denním kurzu. Dne: " + DateTime.Now.ToString());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Nepodařilo se spojit s ČNB. Kurz pro EUR/CZK převod byl nastaven na 25.");
                return 25;
            }
        }
        public async Task<decimal> ExchangeEURtoCZK(decimal value)
        {
            return GetCZKExchangeRate().Result * value;
        }
    }
}
