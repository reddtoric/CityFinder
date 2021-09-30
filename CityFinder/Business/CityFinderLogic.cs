using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using CityFinder.Dtos;
using CityFinder.Models;
using Microsoft.Extensions.Options;

namespace CityFinder.Business
{
    public class CityFinderLogic
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly Keys keys;

        private readonly IMapper _mapper;

        public CityFinderLogic(IOptions<Keys> options, IHttpClientFactory clientFactory, IMapper mapper)
        {
            this.clientFactory = clientFactory;
            this.keys = options.Value;
            this._mapper = mapper;
        }

        public async Task<Location> GetCity(Query query)
        {
            string url = $"https{":"}//app.zipcodebase.com/api/v1/search?apikey={keys.ZipcodebaseApiKey}&codes={query.ZipCode}&country={query.CountryCode}";
            HttpClient client = clientFactory.CreateClient();

            try
            {
                ZipApiResponse response = await client.GetFromJsonAsync<ZipApiResponse>(url);

                if (response.Results.TryGetValue(query.ZipCode, out List<Location> locations))
                {
                    return locations[0];
                }
            }
            catch (System.Exception) { }

            return null;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            string url = "https://restcountries.com/v3.1/all?fields=name,cca2";
            HttpClient client = clientFactory.CreateClient();

            return _mapper.Map<IEnumerable<CountryDto>>(await client.GetFromJsonAsync<IEnumerable<Country>>(url));
        }
    }
}