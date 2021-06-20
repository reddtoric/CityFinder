using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CityFinder.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CityFinder.Business
{
    public class CityFinderLogic
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly Keys keys;

        public CityFinderLogic(IOptions<Keys> options, IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            this.keys = options.Value;
        }

        public async Task<Location> GetCity(Query query)
        {
            string url = $"https{":"}//app.zipcodebase.com/api/v1/search?apikey={keys.ZipApiKey}&codes={query.ZipCode}&country={query.CountryCode}";
            
            HttpClient client = clientFactory.CreateClient();
            ZipApiResponse response = await client.GetFromJsonAsync<ZipApiResponse>(url);

            List<Location> _locations;
            response.Results.TryGetValue(query.ZipCode, out _locations);

            if (_locations != null)
            {
                return _locations[0];
            }

            return null;
        }
    }
}