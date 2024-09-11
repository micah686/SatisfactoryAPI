using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model
{
    public class GetServerOptions
    {
        public Dictionary<string, string> ServerOptions { get; set; }
        public Dictionary<string, string> PendingServerOptions { get; set; }
    }
}
