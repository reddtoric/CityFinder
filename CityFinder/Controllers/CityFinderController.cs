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
        public CityFinderController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<Location>> Get(string country, string zipcode)
        {
            if (country == "United States")
            {
                Location location = await UsaCityFinder.GetCity(new Location() { Country = country, ZipCode = zipcode });

                if (location != null)
                {
                    return Ok(location);
                }
            }

            return NotFound();
        }
    }
}