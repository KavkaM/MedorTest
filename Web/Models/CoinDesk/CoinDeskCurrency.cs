using Newtonsoft.Json;

namespace Web.Models.CoinDesk
{
    public class CoinDeskCurrency
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("rate")]
        public string Rate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rate_float")]
        public decimal RateFloat { get; set; }
    }
}
