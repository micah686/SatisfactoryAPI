using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Endpoints.SetAdminPassword
{
    public class AdminPasswordPayload
    {
        public string Password { get; set; }
        public string AuthenticationToken { get; set; }
    }
}
