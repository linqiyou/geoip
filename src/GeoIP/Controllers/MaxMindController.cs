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
using System.Text;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GeoIP.Controllers
{
    [Route("api/[controller]")]
    public class MaxMindController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly DataSource dataSource;

        public MaxMindController(IHostingEnvironment hostingEnvironment, IOptions<DataSource> dataSource)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.dataSource = dataSource.Value;
        }

        // GET: api/maxmind?ipaddress=123.123.123.123
        [HttpGet]
        public JsonResult GetGeoLocation([FromQuery]string ipAddress)
        {
            var dataSource = $"{this.hostingEnvironment.ContentRootPath}/{this.dataSource.MaxMind}";

            var geolocation = new MaxMindQuery().Query(ipAddress, dataSource);

            return this.Json(geolocation.Result);
        }
    }
}
