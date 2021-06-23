using System.Collections.Generic;
using System.Threading.Tasks;
using CityFinder.Business;
using CityFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityFinder.Controllers
{
    [ApiController]
    [Route("api")]
    public class CityFinderController : ControllerBase
    {
        private readonly CityFinderLogic _cityFinderLogic;

        public CityFinderController(CityFinderLogic cityFinderLogic)
        {
            _cityFinderLogic = cityFinderLogic;
        }

        // GET: api/city?country={country 2 letter code}&zipcode={postal code}
        [HttpGet("city", Name ="GetCity")]
        public async Task<ActionResult<Location>> Get(string country, string zipcode)
        {
            return Ok(await _cityFinderLogic.GetCity(new Query() { CountryCode = country, ZipCode = zipcode }));
        }

        // GET: api/countries
        [HttpGet("countries", Name ="GetCountries")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return Ok(await _cityFinderLogic.GetCountries());
        }
    }
}