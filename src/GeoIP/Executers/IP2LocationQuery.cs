using GeoIP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeoIP.Executers
{
    public class IP2LocationQuery : IQuery
    {
        public async Task<string> Query(string ipAddress, string dataSource)
        {
            string url = $"{dataSource}?ipaddress={ipAddress}";

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                var queriedResult = await content.ReadAsStringAsync();

                var json = JObject.Parse(queriedResult);

                Object result;
                var errorMessage = (string)json["ErrorMessage"];
                if (errorMessage == null)
                {
                    result = new GeoLocation()
                    {
                        IPAddress = (string)json["IPAddress"],
                        City = (string)json["City"],
                        Country = (string)json["Country"],
                        Latitude = (double)json["Latitude"],
                        Longitude = (double)json["Longitude"]
                    };
                }
                else
                {
                    result = new Error()
                    {
                        ErrorMessage = errorMessage
                    };
                }

                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
