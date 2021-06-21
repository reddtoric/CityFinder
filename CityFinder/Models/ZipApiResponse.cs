using System.Collections.Generic;

namespace CityFinder.Models
{
    /// <summary>
    /// Response from zipcodebase.com Postal Code to location information
    /// </summary>
    public class ZipApiResponse
    {
        public Query Query { get; set; }

        public Dictionary<string, List<Location>> Results { get; set; }
    }
}