using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaxMind.GeoIP2;
using GeoIP.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using GeoIP.Utils;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GeoIP.Controllers
{
    [Route("api/[controller]")]
    public class MaxMindController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public MaxMindController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: api/maxmind?ipaddress=123.123.123.123
        [HttpGet]
        public string GetGeoLocation([FromQuery]string ipAddress)
        {
            var error = new Validation()
                                .IsNotNullOrEmpty(ipAddress)
                                .IsIPAddress(ipAddress)
                                .Validate();

            if (error != null)
            {
                return JsonConvert.SerializeObject(error);
            }

            using (var reader = new DatabaseReader(this.hostingEnvironment.ContentRootPath + "/Databases/Maxmind/GeoLite2-City.mmdb"))
            {
                Object result;
                var city = reader.City(ipAddress);
                
                if (city != null)
                {
                    result = new GeoLocation()
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
                        ErrorMessage = "Geo location not found"
                    };
                }

                return JsonConvert.SerializeObject(result);
                
            }
        }
    }
}
