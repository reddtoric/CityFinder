using System.Threading.Tasks;
using CityFinder.Models;
using CityFinder.Support;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public class UsaCityFinder
    {
        private readonly string _zipApiKey = null;

        public UsaCityFinder(IConfiguration configuration)
        {
            _zipApiKey = configuration["ZipApiKey"];
        }

        public async Task<Location> GetCity(Location location)
        {
            string url = $"https{":"}//service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={_zipApiKey}";

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