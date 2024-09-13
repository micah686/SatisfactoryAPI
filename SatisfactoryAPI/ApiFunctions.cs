using SatisfactoryAPI.Model;
using SatisfactoryAPI.Model.Endpoints.ApplyAdvancedGameSettings;
using SatisfactoryAPI.Model.Endpoints.ApplyServerOptions;
using SatisfactoryAPI.Model.Endpoints.AutoLoadSessionName;
using SatisfactoryAPI.Model.Endpoints.ClaimServer;
using SatisfactoryAPI.Model.Endpoints.DeleteSaveFile;
using SatisfactoryAPI.Model.Endpoints.DeleteSaveSession;
using SatisfactoryAPI.Model.Endpoints.EnumerateSessions;
using SatisfactoryAPI.Model.Endpoints.GetAdvancedGameSettings;
using SatisfactoryAPI.Model.Endpoints.GetServerOptions;
using SatisfactoryAPI.Model.Endpoints.GetServerState;
using SatisfactoryAPI.Model.Endpoints.HealthCheck;
using SatisfactoryAPI.Model.Endpoints.PasswordlessLogin;
using SatisfactoryAPI.Model.Endpoints.PasswordLogin;
using SatisfactoryAPI.Model.Endpoints.RenameServer;
using SatisfactoryAPI.Model.Endpoints.RunCommand;
using SatisfactoryAPI.Model.Endpoints.SaveGame;
using SatisfactoryAPI.Model.Endpoints.SetAdminPassword;
using SatisfactoryAPI.Model.Endpoints.SetClientPassword;
using SatisfactoryAPI.Model.Enums;
using System.Dynamic;
using SatisfactoryAPI.Model.Endpoints.DownloadSave;
using SatisfactoryAPI.Model.InProgress;

namespace SatisfactoryAPI
{
    public static class ApiFunctions
    {

        public static async Task CreateGame(this DedicatedServerApiClient apiClient, DataNewGame newGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.CreateNewGame, newGame);
        }








        //-----------------------------------------------------------------------------------------------
        #region Cleaned Up APIs
        public static async Task<HealthCheckResponse> HealthCheck(this DedicatedServerApiClient apiClient, HealthCheckPayload healthCheck)
        {
            return await apiClient.SendRequest<HealthCheckResponse>(ApiCallName.HealthCheck, healthCheck);
        }
        public static async Task VerifyAuthenticationToken(this DedicatedServerApiClient apiClient)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.VerifyAuthenticationToken, null);
        }
        public static async Task<PasswordlessLoginResponse> PasswordlessLogin(this DedicatedServerApiClient apiClient, PasswordlessLoginPayload login)
        {
            return await apiClient.SendRequest<PasswordlessLoginResponse>(ApiCallName.PasswordlessLogin, login);
        }
        public static async Task<string> PasswordLogin(this DedicatedServerApiClient apiClient, PrivilegeLevel privilegeLevel, string password)
        {
            var response = await apiClient.SendRequest<PasswordLoginResponse>(ApiCallName.PasswordLogin, new { MinimumPrivilegeLevel = privilegeLevel.ToString(), Password = password });
            return response.AuthenticationToken;
        }
        public static async Task<ServerStateResponse> GetServerState(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<ServerStateResponse>(ApiCallName.QueryServerState, null);
        }
        public static async Task<ServerOptionsResponse> GetServerOptions(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<ServerOptionsResponse>(ApiCallName.GetServerOptions, null);
        }
        public static async Task<AdvancedGameSettingsResponse> GetAdvancedGameSettings(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<AdvancedGameSettingsResponse>(ApiCallName.GetAdvancedGameSettings, null);
        }
        public static async Task ApplyAdvancedGameSettings(this DedicatedServerApiClient apiClient, AdvancedGameSettingsPayload gameSettings)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyAdvancedGameSettings, gameSettings);
        }
        public static async Task<ClaimServerResponse> ClaimServer(this DedicatedServerApiClient apiClient, ClaimServerPayload claim)
        {
            return await apiClient.SendRequest<ClaimServerResponse>(ApiCallName.ClaimServer, claim);
        }
        public static async Task RenameServer(this DedicatedServerApiClient apiClient, RenameServerPayload newServerName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.RenameServer, newServerName);
        }
        public static async Task SetClientPassword(this DedicatedServerApiClient apiClient, ClientPasswordPayload clientPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetClientPassword, clientPassword);
        }
        public static async Task SetAdminPassword(this DedicatedServerApiClient apiClient, AdminPasswordPayload adminPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAdminPassword, adminPassword);
        }
        public static async Task SetAutoLoadSessionName(this DedicatedServerApiClient apiClient, AutoLoadSessionNamePayload sessionName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAutoLoadSessionName, sessionName);
        }
        public static async Task<CommandResultResponse> RunCommand(this DedicatedServerApiClient apiClient, RunCommandPayload runCommand)
        {
            return await apiClient.SendRequest<CommandResultResponse>(ApiCallName.RunCommand, runCommand);
        }
        public static async Task Shutdown(this DedicatedServerApiClient apiClient)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.Shutdown, null);
        }
        public static async Task ApplyServerOptions(this DedicatedServerApiClient apiClient, ServerOptionsPayload serverOptions)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyServerOptions, serverOptions);
        }
        //CreateNewGame
        public static async Task SaveGame(this DedicatedServerApiClient apiClient, SaveGamePayload saveGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SaveGame, saveGame);
        }
        public static async Task DeleteSave(this DedicatedServerApiClient apiClient, DeleteSaveFilePayload saveFileName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveFile, saveFileName);
        }
        public static async Task DeleteSaveSession(this DedicatedServerApiClient apiClient, DeleteSaveSessionPayload saveSession)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveSession, saveSession);
        }
        public static async Task<EnumerateSessionsResponse> EnumerateSessions(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<EnumerateSessionsResponse>(ApiCallName.EnumerateSessions, null);
        }
        //LoadGame
        public static async Task UploadSave(this DedicatedServerApiClient apiClient, string filePath, UploadSaveGameRequest uploadSave)
        {
            await using var fs = File.OpenRead(filePath);
            await apiClient.UploadSaveFile(ApiCallName.UploadSaveGame, uploadSave, fs, uploadSave.SaveName);
        }
        public static async Task DownloadSave(this DedicatedServerApiClient apiClient, DownloadSavePayload save, string exportLocationPath)
        {
            var response = await apiClient.DownloadSave(ApiCallName.DownloadSaveGame, save);
            await using var saveGameStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = File.Create(exportLocationPath);
            await saveGameStream.CopyToAsync(fileStream);
        }
        #endregion
    }
}
