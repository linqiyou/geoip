using GeoIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeoIP.Utils
{
    public class Validation
    {
        private Error error;

        public Validation IsNotNullOrEmpty(string input)
        {
            if (this.error != null)
            {
                return this;
            }

            if (string.IsNullOrEmpty(input))
            {
                this.error = new Error()
                {
                    ErrorMessage = "Empty input provided"
                };

            }

            return this;
        }

        public Validation IsIPAddress(string input)
        {
            if (this.error != null)
            {
                return this;
            }

            try
            {
                IPAddress.Parse(input);
            }
            catch(FormatException)
            {
                this.error = new Error()
                {
                    ErrorMessage = "Invalid IP address provided"
                };
            }

            return this;
        }

        public Error Validate()
        {
            return this.error;
        }
    }
}
