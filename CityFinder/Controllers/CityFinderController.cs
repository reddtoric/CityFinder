using System.Threading.Tasks;
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
        public async Task<Location> Get(string country, string zipcode)
        {
            return null;
        }
    }
}