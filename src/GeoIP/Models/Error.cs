using GeoIP.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Models
{
    public class Error
    {
        [JsonProperty(PropertyName = ResultMembers.ErrorMessage)]
        public string ErrorMessage { get; set; }
    }
}
