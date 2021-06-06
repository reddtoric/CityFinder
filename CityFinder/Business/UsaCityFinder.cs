using System.Threading.Tasks;
using CityFinder.Models;
using CityFinder.Support;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public static class UsaCityFinder
    {
        private static readonly string zipApiKey = "js-fee1752ae90c41faa7900028379599e3";

        public static async Task<Location> GetCity(Location location)
        {
            // Build url
            string url = $"https://service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={zipApiKey}&fields=geolocation,population";

            // Retrieve location data from zipapi.us
            string data = await HttpAccess.GetContentAsync(url);

            if (data != null)
            {
                // Convert string to json object
                var dataObject = JObject.Parse(data);

                // Add retrieved city into return object
                location.City = dataObject.GetValue("data").ToObject<Location>().City;

                return location;
            }

            return null;
        }
    }
}