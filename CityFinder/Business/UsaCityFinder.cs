using System.Net.Http;
using System.Threading.Tasks;
using CityFinder.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public class UsaCityFinder
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly Keys keys;

        public UsaCityFinder(IOptions<Keys> options, IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            this.keys = options.Value;
        }

        public async Task<Location> GetCity(Location location)
        {
            string url = $"https{":"}//service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={keys.ZipApiKey}&fields=geolocation";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                var data = responseObject.GetValue("data");

                if (data != null)
                {
                    var retrievedData = data.ToObject<Location>();
                    return new Location()
                    {
                        City = retrievedData.City,
                        Country = location.Country,
                        ZipCode = location.ZipCode,
                        IsFound = true,
                        Latitude = retrievedData.Latitude,
                        Longitude = retrievedData.Longitude
                    };
                }
            }

            // City not found, return original data
            return location;
        }
    }
}