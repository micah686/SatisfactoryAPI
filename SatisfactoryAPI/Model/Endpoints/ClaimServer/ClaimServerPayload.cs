using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Endpoints.ClaimServer
{
    public class ClaimServerPayload
    {
        public string ServerName { get; set; }
        public string AdminPassword { get; set; }
    }
}
