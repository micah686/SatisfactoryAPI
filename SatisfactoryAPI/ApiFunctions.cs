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
using SatisfactoryAPI.Model.Endpoints.CreateNewGame;
using SatisfactoryAPI.Model.Endpoints.DownloadSave;
using SatisfactoryAPI.Model.Endpoints.LoadSave;
using SatisfactoryAPI.Model.Endpoints.UploadSave;

namespace SatisfactoryAPI
{
    public static class ApiFunctions
    {

        

        #region APIs
        /// <summary>
        /// Performs a health check on the Dedicated Server API. Allows passing additional data between Modded Dedicated Server and Modded Game Client.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="healthCheck">Custom Data passed from the Game Client or Third Party service. Not used by vanilla Dedicated Servers</param>
        /// <returns>Returns a <see cref="HealthCheckResponse"/> of a Health status, and any custom health info from modded servers</returns>
        public static async Task<HealthCheckResponse> HealthCheck(this DedicatedServerApiClient apiClient, HealthCheckPayload healthCheck)
        {
            return await apiClient.SendRequest<HealthCheckResponse>(ApiCallName.HealthCheck, healthCheck);
        }
        /// <summary>
        /// Verifies the Authentication token provided to the Dedicated Server API.
        /// </summary>
        /// <param name="apiClient"></param>
        public static async Task VerifyAuthenticationToken(this DedicatedServerApiClient apiClient)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.VerifyAuthenticationToken, null);
        }
        /// <summary>
        /// Attempts to perform a passwordless login to the Dedicated Server as a player. Passwordless login is possible if the Dedicated Server is not claimed,
        ///or if Client Protection Password is not set for the Dedicated Server.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="login">Contains the minimum privilege level to attempt to acquire by logging in.</param>
        /// <returns>Returns the authentication Token if login is successful </returns>
        public static async Task<PasswordlessLoginResponse> PasswordlessLogin(this DedicatedServerApiClient apiClient, PasswordlessLoginPayload login)
        {
            return await apiClient.SendRequest<PasswordlessLoginResponse>(ApiCallName.PasswordlessLogin, login);
        }
        /// <summary>
        /// Attempts to log in to the Dedicated Server as a player using either Admin Password or Client Protection Password.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="privilegeLevel">Minimum privilege level to attempt to acquire by logging in.</param>
        /// <param name="password">Password to attempt to log in with, in plaintext </param>
        /// <returns>Returns the authentication Token if login is successful</returns>
        public static async Task<string> PasswordLogin(this DedicatedServerApiClient apiClient, PrivilegeLevel privilegeLevel, string password)
        {
            var response = await apiClient.SendRequest<PasswordLoginResponse>(ApiCallName.PasswordLogin, new { MinimumPrivilegeLevel = privilegeLevel.ToString(), Password = password });
            return response.AuthenticationToken;
        }
        /// <summary>
        /// Retrieves the current state of the Dedicated Server. Does not require any input parameters.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <returns>Returns a <see cref="ServerStateResponse"/> of the state of the dedicated server</returns>
        public static async Task<ServerStateResponse> GetServerState(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<ServerStateResponse>(ApiCallName.QueryServerState, null);
        }
        /// <summary>
        /// Retrieves currently applied server options and server options that are still pending application (because of needing session or server restart)
        /// </summary>
        /// <param name="apiClient"></param>
        /// <returns>Returns server options both set and pending on the server</returns>
        public static async Task<ServerOptionsResponse> GetServerOptions(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<ServerOptionsResponse>(ApiCallName.GetServerOptions, null);
        }
        /// <summary>
        /// Retrieves currently applied advanced game settings. Does not require input parameters.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <returns>Returns AGS (Advanced Game Settings) set on the server</returns>
        public static async Task<AdvancedGameSettingsResponse> GetAdvancedGameSettings(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<AdvancedGameSettingsResponse>(ApiCallName.GetAdvancedGameSettings, null);
        }
        /// <summary>
        /// Applies new values to the provided Advanced Game Settings properties. Will automatically enable Advanced Game Settings
        ///for the currently loaded save if they are not enabled already.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="gameSettings">List of AGS (Advanced Game Settings) to apply</param>
        public static async Task ApplyAdvancedGameSettings(this DedicatedServerApiClient apiClient, AdvancedGameSettingsPayload gameSettings)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyAdvancedGameSettings, gameSettings);
        }
        /// <summary>
        /// Claims this Dedicated Server if it is not claimed. Requires InitialAdmin privilege level, which can only be acquired by attempting passwordless login
        ///while the server does not have an Admin Password set, e.g. it is not claimed yet.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="claim">The Server name and admin password required to claim a server</param>
        /// <returns>Returns the new authentication token. The client should drop InitialAdmin privileges after that
        ///and use returned AuthenticationToken instead, and update it's cached server game state by calling QueryServerState.</returns>
        public static async Task<ClaimServerResponse> ClaimServer(this DedicatedServerApiClient apiClient, ClaimServerPayload claim)
        {
            return await apiClient.SendRequest<ClaimServerResponse>(ApiCallName.ClaimServer, claim);
        }
        /// <summary>
        /// Renames the Dedicated Server once it has been claimed. Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="newServerName">Payload of the new server name</param>
        public static async Task RenameServer(this DedicatedServerApiClient apiClient, RenameServerPayload newServerName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.RenameServer, newServerName);
        }
        /// <summary>
        /// Updates the currently set Client Protection Password. This will invalidate all previously issued Client authentication tokens.
        ///Pass empty string to remove the password, and let anyone join the server as Client.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="clientPassword">Payload of the client password, with the password in plaintext</param>
        public static async Task SetClientPassword(this DedicatedServerApiClient apiClient, ClientPasswordPayload clientPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetClientPassword, clientPassword);
        }
        /// <summary>
        /// Updates the currently set Admin Password. This will invalidate all previously issued Client and Admin authentication tokens.
        ///Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="adminPassword">Payload of the admin password, with the password in plaintext</param>
        public static async Task SetAdminPassword(this DedicatedServerApiClient apiClient, AdminPasswordPayload adminPassword)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAdminPassword, adminPassword);
        }
        /// <summary>
        /// pdates the name of the session that the Dedicated Server will automatically load on startup. Does not change currently loaded session.
        ///Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="sessionName">Payload of the session name to use</param>
        public static async Task SetAutoLoadSessionName(this DedicatedServerApiClient apiClient, AutoLoadSessionNamePayload sessionName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SetAutoLoadSessionName, sessionName);
        }
        /// <summary>
        /// Runs the given Console Command on the Dedicated Server, and returns it's output to the Console. Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="runCommand">Payload of the command string to run</param>
        /// <returns>Returns the response result from the command</returns>
        public static async Task<CommandResultResponse> RunCommand(this DedicatedServerApiClient apiClient, RunCommandPayload runCommand)
        {
            return await apiClient.SendRequest<CommandResultResponse>(ApiCallName.RunCommand, runCommand);
        }
        /// <summary>
        /// Shuts down the Dedicated Server. If automatic restart script is setup, this allows restarting the server to apply new settings or update.
        ///Requires Admin privileges. Shutdowns initiated by remote hosts are logged with their IP and their token.
        /// </summary>
        /// <param name="apiClient"></param>
        public static async Task Shutdown(this DedicatedServerApiClient apiClient)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.Shutdown, null);
        }
        /// <summary>
        /// Applies new Server Options to the Dedicated Server. Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="serverOptions">List of server options to apply</param>
        public static async Task ApplyServerOptions(this DedicatedServerApiClient apiClient, ServerOptionsPayload serverOptions)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.ApplyServerOptions, serverOptions);
        }
        /// <summary>
        /// Creates a new session on the Dedicated Server, and immediately loads it.
        /// HTTPS API becomes temporarily unavailable when map loading is in progress
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="newGame">Payload of options for the new game</param>
        public static async Task CreateGame(this DedicatedServerApiClient apiClient, CreateNewGamePayload newGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.CreateNewGame, newGame);
        }
        /// <summary>
        /// Saves the currently loaded session into the new save game file named as the argument.
        ///Requires Admin privileges. SaveName might be changed to satisfy file system restrictions on file names.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="saveGame">Payload of the name for the Save</param>
        public static async Task SaveGame(this DedicatedServerApiClient apiClient, SaveGamePayload saveGame)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.SaveGame, saveGame);
        }
        /// <summary>
        /// Deletes the existing save game file from the server. Requires Admin privileges. SaveName might be changed to satisfy file system
        ///restrictions on file names.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="saveFileName">Payload of the name of the Save to delete</param>
        public static async Task DeleteSave(this DedicatedServerApiClient apiClient, DeleteSaveFilePayload saveFileName)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveFile, saveFileName);
        }
        /// <summary>
        /// Deletes all save files belonging to the specific session name. Requires Admin privileges. SessionName must be
        ///a valid session name with at least one saved save game file belonging to it.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="saveSession">Payload of the name of the Session to delete</param>
        public static async Task DeleteSaveSession(this DedicatedServerApiClient apiClient, DeleteSaveSessionPayload saveSession)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.DeleteSaveSession, saveSession);
        }
        /// <summary>
        /// Enumerates all save game files available on the Dedicated Server. Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <returns>Returns a list of all of the sessions</returns>
        public static async Task<EnumerateSessionsResponse> EnumerateSessions(this DedicatedServerApiClient apiClient)
        {
            return await apiClient.SendRequest<EnumerateSessionsResponse>(ApiCallName.EnumerateSessions, null);
        }
        /// <summary>
        /// Loads the save game file by name, optionally with Advanced Game Settings enabled. Requires Admin privileges.
        ///Dedicated Server HTTPS API will become temporarily unavailable when save game is being loaded.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="loadSavePayload">Payload of the options for loading the game.</param>
        public static async Task LoadGame(this DedicatedServerApiClient apiClient, LoadSavePayload loadSavePayload)
        {
            await apiClient.SendRequest<ExpandoObject>(ApiCallName.LoadGame, loadSavePayload);
        }
        /// <summary>
        /// Uploads save game file to the Dedicated Server with the given name, and optionally immediately loads it.
        ///Requires Admin privileges. If save file is immediately loaded, Dedicated Server HTTPS API will become temporarily unavailable until save game is loaded.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="filePath">Local path to the save file to upload</param>
        /// <param name="uploadSave">Payload of the settings of the save file being uploaded</param>
        public static async Task UploadSave(this DedicatedServerApiClient apiClient, string filePath, UploadSaveGameRequest uploadSave)
        {
            await using var fs = File.OpenRead(filePath);
            await apiClient.UploadSaveFile(ApiCallName.UploadSaveGame, uploadSave, fs, uploadSave.SaveName);
        }
        /// <summary>
        /// Downloads save game with the given name from the Dedicated Server. Requires Admin privileges.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="save">Name of the save to download</param>
        /// <param name="exportLocationPath">Path to where you want the save file to be exported to</param>
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
