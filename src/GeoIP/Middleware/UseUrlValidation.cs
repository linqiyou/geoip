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
            var regex = new Regex(endpointValidationRegex, RegexOptions.IgnoreCase);
            if (regex.Match(httpContext.Request.Path).Success)
            {
                var ipaddress = httpContext.Request.Query.FirstOrDefault(query => query.Key == "ipaddress");
                if (string.IsNullOrEmpty(ipaddress.Value))
                {
                    var error = new Error()
                    {
                        ErrorMessage = ErrorMessages.IPAddressNotProvided
                    };

                    httpContext.Response.ContentType = ContentTypes.ApplicationJson;

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
                    return;
                }

                try
                {
                    IPAddress.Parse(ipaddress.Value);
                }
                catch (FormatException)
                {
                    var error = new Error()
                    {
                        ErrorMessage = ErrorMessages.InvalidIPAddressProvided
                    };

                    httpContext.Response.ContentType = ContentTypes.ApplicationJson;

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
                    return;
                }

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
