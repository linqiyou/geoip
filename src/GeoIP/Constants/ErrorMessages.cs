using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Constants
{
    public static class ErrorMessages
    {
        public const string IPAddressNotProvided = "IP address not provided";
        public const string InvalidIPAddressProvided = "Invalid IP address Provided";

        public const string DomainNameNotProvided = "Domain name not provided";
        public const string InvalidDomainNameProvided = "Invalid domain name provided";

        public const string UnableToResolveDomainName = "Unable to resolve domain name";
        public const string EitherIPAddressOrDomainNameMustBeProvided = "Either IP address or domain name must be provided";
    }
}
