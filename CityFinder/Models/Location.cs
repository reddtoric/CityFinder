namespace CityFinder.Models
{
    public class Location
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public bool IsFound { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}