using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoIP.Models;
using GeoIP.Constants;

namespace GeoIP.Utils
{
    public static class DomainNameValidator
    {
        public static Error Validate(string input)
        {
            if (!input.IsValidDnsName())
            {
                return new Error()
                {
                    ErrorMessage = ErrorMessages.InvalidDomainNameProvided
                };
            }
            else
            {
                return null;
            }
        }
    }
}
