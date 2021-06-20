namespace CityFinder.Models
{
    /// <summary>
    /// Query for zipcodebase.com
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 2-letter country code
        /// </summary>
        public string CountryCode { get; set; }

        // Response from zipcodebase.com api is an string array of zipcodes but since only requesting with 1 zipcode and country, it should return just 1
        public string ZipCode { get; set; }
    }
}