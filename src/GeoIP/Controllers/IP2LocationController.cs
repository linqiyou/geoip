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
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GeoIP.Controllers
{
    [Route("api/[controller]")]
    public class IP2LocationController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly DataSource dataSource;

        public IP2LocationController(IHostingEnvironment hostingEnvironment, IOptions<DataSource> dataSource)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.dataSource = dataSource.Value;
        }

        // GET api/ip2location?ipaddress=123.123.123.123
        [HttpGet]
        public JsonResult GetGeoLocation([FromQuery]string ipAddress)
        {
            var dataSource = this.dataSource.IP2Location;

            var geolocation = new IP2LocationQuery().Query(ipAddress, dataSource);

            return this.Json(geolocation.Result);
        }
    }
}
