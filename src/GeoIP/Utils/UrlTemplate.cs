using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Utils
{
    public static class UrlTemplate
    {
        public static string GetEndpointUrl(string baseUrl, string ipAddress)
            => $"{baseUrl}?ipaddress={ipAddress}";
    }
}
