using Newtonsoft.Json;

namespace KraerApp.Models
{
    public class Address
    {
        
        [JsonProperty("StreetName")]
        public string StreetName { get; set; } //vej navn
        [JsonProperty("StreetNumber")]
        public string StreetNumber { get; set; } //husnumber
        [JsonProperty("Floor")]
        public string Floor { get; set; } //etage
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; } //postnummer
        
    }
}