using Newtonsoft.Json;

namespace QuotesModel
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CurrencyJSON
    {
        public string CurrencyKey { get; set; }

        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "NumCode")]
        public int NumCode { get; set; }

        [JsonProperty(PropertyName = "CharCode")]
        public string CharCode { get; set; }

        [JsonProperty(PropertyName = "Nominal")]
        public decimal Nominal { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Value")]
        public decimal Value { get; set; }

        [JsonProperty(PropertyName = "Previous")]
        public decimal Previous { get; set; }
    }
}
