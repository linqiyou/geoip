using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaxMind.GeoIP2;
using GeoIP.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

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

        public IActionResult Index()
        {
            return View();
        }

        // GET: api/maxmind/id
        [HttpGet("{id}")]
        public string Get(string id)
        {
            using (var reader = new DatabaseReader(this.hostingEnvironment.ContentRootPath + "/Databases/Maxmind/GeoLite2-City.mmdb"))
            {
                Object result;
                var city = reader.City(id);
                
                if (city != null)
                {
                    result = new GeoLocation()
                    {
                        IPAddress = id,
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
                        ErrorMessage = "GeoIP not found"
                    };
                }

                return JsonConvert.SerializeObject(result);
                
            }
        }
    }
}
