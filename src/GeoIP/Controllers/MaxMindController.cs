using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaxMind.GeoIP2;
using GeoIP.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using GeoIP.Executers;

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
            var dataSource = this.hostingEnvironment.ContentRootPath + "/Databases/MaxMind/GeoLite2-City.mmdb";

            var geolocation = new MaxMindQuery().Query(ipAddress, dataSource);

            return geolocation.Result;
        }
    }
}
