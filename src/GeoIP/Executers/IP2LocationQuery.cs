using GeoIP.Models;
using GeoIP.Utils;
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
        public async Task<object> Query(string ipAddress, string dataSource)
        {
            string url = UrlTemplate.GetEndpointUrl(dataSource, ipAddress);

            object result;

            try
            {
                using (var client = new HttpClient())
                using (var response = await client.GetAsync(dataSource))
                using (var content = response.Content)
                {
                    var queriedResult = await content.ReadAsStringAsync();

                    var json = JObject.Parse(queriedResult);

                    var errorMessage = (string)json["error_message"];
                    if (errorMessage == null)
                    {
                        result = new Geolocation()
                        {
                            IPAddress = (string)json["ipaddress"],
                            City = (string)json["city"],
                            Country = (string)json["country"],
                            Latitude = (double?)json["latitude"],
                            Longitude = (double?)json["longitude"]
                        };
                    }
                    else
                    {
                        result = new Error()
                        {
                            ErrorMessage = errorMessage
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result = new Error()
                {
                    ErrorMessage = ex.Message
                };
            }

            return result;
        }
    }
}
