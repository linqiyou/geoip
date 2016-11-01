using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoIP.Executers
{
    interface IQuery
    {
        Task<object> Query(string ipAddress, string dataSource);
    }
}
