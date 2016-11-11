using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Net;
using GeoIP.Models;
using Newtonsoft.Json;
using GeoIP.Constants;
using GeoIP.Utils;

namespace GeoIP.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UseUrlValidation
    {
        private const string endpointValidationRegex = @"^/api/(?:maxmind|ip2location)$";

        private readonly RequestDelegate next;

        public UseUrlValidation(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Error error = null;

            var regex = new Regex(endpointValidationRegex, RegexOptions.IgnoreCase);
            if (regex.Match(httpContext.Request.Path).Success)
            {
                var ipaddress = httpContext.Request.Query.FirstOrDefault(query => query.Key == "ipaddress").Value;
                var domainName = httpContext.Request.Query.FirstOrDefault(query => query.Key == "domainname").Value;

                if (string.IsNullOrEmpty(ipaddress) && string.IsNullOrEmpty(domainName))
                {
                    error = new Error()
                    {
                        ErrorMessage = ErrorMessages.EitherIPAddressOrDomainNameMustBeProvided
                    };
                }

                if (!string.IsNullOrEmpty(ipaddress) && string.IsNullOrEmpty(domainName))
                {
                    error = IPAddressValidator.Validate(ipaddress);
                }

                if (!string.IsNullOrEmpty(domainName) && string.IsNullOrEmpty(ipaddress))
                {
                    error = DomainNameValidator.Validate(domainName);
                }
            }

            if (error != null)
            {
                httpContext.Response.ContentType = ContentTypes.ApplicationJson;

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
                return;
            }

            await this.next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UseUrlValidationExtensions
    {
        public static IApplicationBuilder UseUseUrlValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseUrlValidation>();
        }
    }
}
