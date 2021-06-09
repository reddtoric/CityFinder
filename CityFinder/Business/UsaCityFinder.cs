using System.Net.Http;
using System.Threading.Tasks;
using CityFinder.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public class UsaCityFinder
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Keys _keys;

        public UsaCityFinder(IOptions<Keys> options, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _keys = options.Value;
        }

        public async Task<Location> GetCity(Location location)
        {
            string url = $"https{":"}//service.zipapi.us/zipcode/{location.ZipCode}?X-API-KEY={_keys.ZipApiKey}";
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