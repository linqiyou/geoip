using GeoIP.Constants;
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

                    var errorMessage = (string)json[ResultMembers.ErrorMessage];
                    if (errorMessage == null)
                    {
                        result = new Geolocation()
                        {
                            IPAddress = (string)json[ResultMembers.IPAddress],
                            City = (string)json[ResultMembers.City],
                            Country = (string)json[ResultMembers.Country],
                            Latitude = (double?)json[ResultMembers.Latitude],
                            Longitude = (double?)json[ResultMembers.Longitude]
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
