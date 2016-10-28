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

namespace GeoIP.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UseUrlValidation
    {
        private readonly RequestDelegate _next;

        public UseUrlValidation(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var regex = new Regex(@"^/api/(?:maxmind|ip2location)$", RegexOptions.IgnoreCase);
            if (regex.Match(httpContext.Request.Path).Success)
            {
                var ipaddress = httpContext.Request.Query.FirstOrDefault(query => query.Key == "ipaddress");
                if (string.IsNullOrEmpty(ipaddress.Value))
                {
                    var error = new Error()
                    {
                        ErrorMessage = "IP address not provided"
                    };

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
                        ErrorMessage = "Invalid IP address provided"
                    };

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
                    return;
                }

            }

            await _next.Invoke(httpContext);
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
