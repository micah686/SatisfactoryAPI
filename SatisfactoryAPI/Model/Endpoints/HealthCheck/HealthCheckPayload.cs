using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Endpoints.HealthCheck
{
    public class HealthCheckPayload
    {
        public string ClientCustomData { get; set; } = string.Empty;
    }
}
