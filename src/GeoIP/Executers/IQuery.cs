using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Executers
{
    interface IQuery
    {
        Task<string> Query(string ipAddress, string dataSource);
    }
}
