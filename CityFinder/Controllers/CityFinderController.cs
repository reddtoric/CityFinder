using System.Threading.Tasks;
using CityFinder.Business;
using CityFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityFinder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityFinderController : ControllerBase
    {
        private readonly CityFinderLogic _cityFinderLogic;

        public CityFinderController(CityFinderLogic cityFinderLogic)
        {
            _cityFinderLogic = cityFinderLogic;
        }

        [HttpGet]
        public async Task<ActionResult<Location>> Get(string country, string zipcode)
        {
            return Ok(await _cityFinderLogic.GetCity(new Query() { CountryCode = country, ZipCode = zipcode }));
        }
    }
}