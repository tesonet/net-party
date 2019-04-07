using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.core.DataAccess
{
    class ApiSettings : IApiSettings
    {
        public string ServerUri { get; set; }
        public string TokenUri { get; set; }
    }
}
