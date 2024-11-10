using Newtonsoft.Json;

namespace Web.Models.CoinDesk
{
    public class CoinDeskTime
    {
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("updatedISO")]
        public DateTime UpdatedISO { get; set; }
        [JsonProperty("updateduk")]
        public string UpdatedUK { get; set; }
    }
}
