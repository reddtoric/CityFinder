using System.Text.Json.Serialization;

namespace CityFinder.Models
{
    public class Country
    {
        public string Cca2 { get; set; }

        public Name Name { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("official")]
        public string Offical { get; set; }
    }
}