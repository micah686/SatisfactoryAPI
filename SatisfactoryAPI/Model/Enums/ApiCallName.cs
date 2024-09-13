
namespace SatisfactoryAPI.Model.Enums
{
    public enum ApiCallName
    {
        HealthCheck,
        VerifyAuthenticationToken,
        PasswordlessLogin,
        PasswordLogin, 
        QueryServerState,
        GetServerOptions,
        GetAdvancedGameSettings,
        ApplyAdvancedGameSettings,
        ClaimServer, 
        RenameServer,
        SetClientPassword,
        SetAdminPassword,
        SetAutoLoadSessionName, 
        RunCommand, 
        Shutdown, 
        ApplyServerOptions, 
        CreateNewGame, 
        SaveGame, 
        DeleteSaveFile, 
        DeleteSaveSession, 
        EnumerateSessions, 
        LoadGame, 
        UploadSaveGame, 
        DownloadSaveGame 
    }
}
