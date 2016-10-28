using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using GeoIP.Models;
using GeoIP.Executers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GeoIP.Controllers
{
    [Route("api/[controller]")]
    public class IP2LocationController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public IP2LocationController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET api/ip2location?ipaddress=123.123.123.123
        [HttpGet]
        public string GetGeoLocation([FromQuery]string ipAddress)
        {
            var dataSource = "http://localhost:3000/ip2location";

            var geolocation = new IP2LocationQuery().Query(ipAddress, dataSource);

            return geolocation.Result;
        }
    }
}
