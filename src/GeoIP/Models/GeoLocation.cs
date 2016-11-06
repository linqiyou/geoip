using GeoIP.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Models
{
    public class Geolocation
    {
        [JsonProperty(PropertyName = ResultMembers.IPAddress)]
        public string IPAddress { get; set; }

        [JsonProperty(PropertyName = ResultMembers.City)]
        public string City { get; set; }

        [JsonProperty(PropertyName = ResultMembers.Country)]
        public string Country { get; set; }

        [JsonProperty(PropertyName = ResultMembers.Latitude)]
        public double? Latitude { get; set; }

        [JsonProperty(PropertyName = ResultMembers.Longitude)]
        public double? Longitude { get; set; }
    }
}
