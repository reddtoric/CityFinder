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
        private readonly UsaCityFinder _usaCityFinder;

        public CityFinderController(UsaCityFinder usaCityFinder)
        {
            _usaCityFinder = usaCityFinder;
        }

        [HttpGet]
        public async Task<ActionResult<Location>> Get(string country, string zipcode)
        {
            Location location = new() { Country = country, ZipCode = zipcode };

            if (country == "United States")
            {
                return Ok(await _usaCityFinder.GetCity(location));
            }

            return Ok(location);
        }
    }
}