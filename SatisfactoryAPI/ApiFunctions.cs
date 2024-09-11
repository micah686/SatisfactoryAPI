using SatisfactoryAPI.Model;
using SatisfactoryAPI.Model.DataPayloads;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI
{
    public static class ApiFunctions
    {
        public static async Task<HealthCheckResponse> HealthCheck(DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<HealthCheckResponse>("HealthCheck", new { ClientCustomData = "" });
        }

        public static async Task<RespServerState> GetServerState(DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespServerState>("QueryServerState", null);
        }

        public static async Task<RespServerOptions> GetServerOptions(DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespServerOptions>("GetServerOptions", null);
        }

        public static async Task<RespAdvancedGameSettings> GetAdvancedGameSettings(DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespAdvancedGameSettings>("GetAdvancedGameSettings", null);
        }

        public static async Task RenameServer(DedicatedServerApiClient apiClient, DataRenameServer newServerName)
        {
            await apiClient.SendRequest<ExpandoObject>("RenameServer", newServerName);
        }
    }
}
