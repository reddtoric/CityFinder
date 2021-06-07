using System.Threading.Tasks;
using CityFinder.Models;
using CityFinder.Support;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public static class UsaCityFinder
    {
        public static async Task<Location> GetCity(Location location)
        {
            // Build url
            string url = $"https://service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={ApiKeys.ZipApiJsKey}&fields=geolocation,population";

            // Retrieve info from zipapi.us
            string response = await HttpAccess.GetContentAsync(url);

            // Convert string to json object
            var responseObject = JObject.Parse(response);

            // Get the data field in the response
            var data = responseObject.GetValue("data");

            if (data != null)
            {
                // Add retrieved city into return object
                location.City = data.ToObject<Location>().City;
                location.IsFound = true;
            }

            return location;
        }
    }
}