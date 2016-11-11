using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoIP.Models;
using GeoIP.Constants;

namespace GeoIP.Utils
{
    public static class IPAddressValidator
    {
        public static Error Validate(string input)
        {
            if (!input.IsValidIPAddress())
            {
                return new Error()
                {
                    ErrorMessage = ErrorMessages.InvalidIPAddressProvided
                };
            }
            else
            {
                return null;
            }
        }
    }
}
