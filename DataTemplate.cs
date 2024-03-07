using System.Text.Json.Serialization;

namespace ApiCacheExample
{
    public class DataTemplate
    {
        [JsonPropertyName("Deal_Name")]
        public string? Deal_Name { get; set; }

        [JsonPropertyName("Exchange_Rate")]
        public int Exchange_Rate { get; set; }

        [JsonPropertyName("Amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("Currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("Locked__s")]
        public bool Locked__s { get; set; }

        [JsonPropertyName("Stage")]
        public string? Stage { get; set; }

        [JsonPropertyName("locked_for_me")]
        public bool locked_for_me { get; set; }

        [JsonPropertyName("Account_Name")]
        public string? Account_Name { get; set; }

        [JsonPropertyName("id")]
        public string? id { get; set; }


    }
}
