using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GeoIP.Utils
{
    public static class DomainNameToIP
    {
        public static async Task<string> Resolve(string domainName)
        {
            try
            {
                var ipAddresses = await Dns.GetHostAddressesAsync(domainName);

                return ipAddresses?[0]?.ToString() ?? null;
            }
            catch (SocketException)
            {
                return null;
            }
        }
    }
}
