using System.Diagnostics;
using System.Net.Http;
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
        private readonly IHttpClientFactory _clientFactory;

        public UsaCityFinder(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _zipApiKey = configuration["ZipApiKey"];
            _clientFactory = clientFactory;
        }

        public async Task<Location> GetCity(Location location)
        {
            string url = $"https{":"}//service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={_zipApiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
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
            }

            // City not found, return original data
            return location;
        }
    }
}