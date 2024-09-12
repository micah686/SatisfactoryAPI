using SatisfactoryAPI.Model;
using SatisfactoryAPI.Model.DataPayloads;
using SatisfactoryAPI.Model.Enums;
using SatisfactoryAPI.Model.Responses;
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
        


        public static async Task SetAutoLoadSessionName(this DedicatedServerApiClient apiClient, DataAutoLoadSessionName sessionName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAutoLoadSessionName, sessionName);
        }
        
        
        public static async Task CreateNewGame(this DedicatedServerApiClient apiClient, DataNewGame newGameOptions)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.CreateNewGame, newGameOptions);
        }
        public static async Task SaveGame(this DedicatedServerApiClient apiClient, DataSaveGame saveGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SaveGame, saveGame);
        }
        
        



        public static async Task LoadGame(this DedicatedServerApiClient apiClient, DataLoadGame loadGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.LoadGame, loadGame);
        }
        public static async Task<ExpandoObject> DownloadSave(this DedicatedServerApiClient apiClient, DataDownloadSave downloadSave)
        {
            return await apiClient.SendRequest<ExpandoObject>(ApiCallName.DownloadSaveGame, downloadSave);
        }





        #region working
        public static async Task<HealthCheckResponse> HealthCheck(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<HealthCheckResponse>(ApiCallName.HealthCheck, new { ClientCustomData = "" });
        }

        public static async Task<string> PasswordLogin(this DedicatedServerApiClient apiClient, PrivilegeLevel privilegeLevel, string password)
        {
            var response = await apiClient.SendRequest<AuthResponse>(ApiCallName.PasswordLogin, new { MinimumPrivilegeLevel = privilegeLevel.ToString(), Password = password });
            return response.AuthenticationToken;
        }
        public static async Task<RespServerState> GetServerState(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespServerState>(ApiCallName.QueryServerState, null);
        }
        public static async Task<RespServerOptions> GetServerOptions(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespServerOptions>(ApiCallName.GetServerOptions, null);
        }
        public static async Task<RespAdvancedGameSettings> GetAdvancedGameSettings(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<RespAdvancedGameSettings>(ApiCallName.GetAdvancedGameSettings, null);
        }
        public static async Task ApplyAdvancedGameSettings(this DedicatedServerApiClient apiClient, DataAdvancedGameSettings gameSettings)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyAdvancedGameSettings, gameSettings);
        }

        public static async Task RenameServer(this DedicatedServerApiClient apiClient, DataRenameServer newServerName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.RenameServer, newServerName);
        }
        public static async Task SetClientPassword(this DedicatedServerApiClient apiClient, DataClientPassword clientPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetClientPassword, clientPassword);
        }
        public static async Task SetAdminPassword(this DedicatedServerApiClient apiClient, DataAdminPassword adminPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAdminPassword, adminPassword);
        }
        public static async Task<RespCommandResult> RunCommand(this DedicatedServerApiClient apiClient, DataRunCommand runCommand)
        {
            return await apiClient.SendRequest<RespCommandResult>(ApiCallName.RunCommand, runCommand);
        }
        public static async Task Shutdown(this DedicatedServerApiClient apiClient)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.Shutdown, null);
        }



        public static async Task<RespPasswordlessLogin> PasswordlessLogin(this DedicatedServerApiClient apiClient, DataPasswordlessLogin login)
        {
            return await apiClient.SendRequest<RespPasswordlessLogin>(ApiCallName.PasswordlessLogin, login);
        }



        public static async Task<RespClaimServer> ClaimServer(this DedicatedServerApiClient apiClient, DataClaimServer claim)
        {
            return await apiClient.SendRequest<RespClaimServer>(ApiCallName.ClaimServer, claim);
        }

        public static async Task DeleteSave(this DedicatedServerApiClient apiClient, DataDeleteSaveFile saveFileName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveFile, saveFileName);
        }

        public static async Task ApplyServerOptions(this DedicatedServerApiClient apiClient, DataServerOptions serverOptions)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyServerOptions, serverOptions);
        }
        public static async Task DeleteSaveSession(this DedicatedServerApiClient apiClient, DataDeleteSaveSession saveSession)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveSession, saveSession);
        }

        #endregion
    }
}
