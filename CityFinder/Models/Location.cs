using System.Text.Json.Serialization;

namespace CityFinder.Models
{
    public class Location
    {
        public string City { get; set; }

        /// <summary>
        /// 2-letter country code
        /// </summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("postal_code")]
        public string ZipCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}