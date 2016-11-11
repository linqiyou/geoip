using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Utils
{
    public static class StringExtension
    {
        public static bool IsValidIPAddress(this string value)
        {
            var type = Uri.CheckHostName(value);
            if (type == UriHostNameType.IPv4 || type == UriHostNameType.IPv6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDnsName(this string value)
        {
            var type = Uri.CheckHostName(value);
            if (type == UriHostNameType.Dns)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
