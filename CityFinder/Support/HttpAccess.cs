using System.Net.Http;
using System.Threading.Tasks;

namespace CityFinder.Support
{
    public static class HttpAccess
    {
        public static async Task<string> GetContentAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(url))
                {
                    using (HttpContent content = res.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}