using GeoIP.Models;
using MaxMind.GeoIP2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Executers
{
    public class MaxMindQuery : IQuery
    {
        public async Task<string> Query(string ipAddress, string dataSource)
        {
            using (var reader = new DatabaseReader(dataSource))
            {
                Object result;
                var city = reader.City(ipAddress);

                if (city != null)
                {
                    result = new Geolocation()
                    {
                        IPAddress = ipAddress,
                        City = city.City.Name,
                        Country = city.Country.Name,
                        Latitude = city.Location.Latitude,
                        Longitude = city.Location.Longitude
                    };
                }
                else
                {
                    result = new Error()
                    {
                        ErrorMessage = "Geolocation not found"
                    };
                }

                return JsonConvert.SerializeObject(result);

            }
        }
    }
}
