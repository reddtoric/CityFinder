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
            string url = $"https{":"}//service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={ApiKeys.ZipApiJsKey}";

            string response = await HttpAccess.GetContentAsync(url);
            var responseObject = JObject.Parse(response);
            var data = responseObject.GetValue("data");

            if (data != null)
            {
                return new Location()
                {
                    City = data.ToObject<Location>().City,
                    Country = location.Country,
                    ZipCode = location.ZipCode,
                    IsFound = true
                };
            }

            // City not found, return original data
            return location;
        }
    }
}