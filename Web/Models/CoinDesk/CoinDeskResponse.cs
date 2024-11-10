using Newtonsoft.Json;

namespace Web.Models.CoinDesk
{
    public class CoinDeskResponse
    {
        [JsonProperty("time")]
        public CoinDeskTime Time { get; set; }
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }
        [JsonProperty("chartName")]
        public string ChartName { get; set; }
        [JsonProperty("bpi")]
        public Dictionary<string, CoinDeskCurrency> Currencies { get; set; }
    }
}
