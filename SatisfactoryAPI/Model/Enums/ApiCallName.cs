using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Enums
{
    public enum ApiCallName
    {
        HealthCheck,//-
        VerifyAuthenticationToken,
        PasswordlessLogin,
        PasswordLogin,
        QueryServerState,//-
        GetServerOptions,//-
        GetAdvancedGameSettings,//-
        ApplyAdvancedGameSettings,//-
        ClaimServer,
        RenameServer,//-
        SetClientPassword,//-
        SetAdminPassword,//-
        SetAutoLoadSessionName, //-
        RunCommand, //-
        Shutdown, //-
        ApplyServerOptions, //-
        CreateNewGame, //-
        SaveGame, //-
        DeleteSaveFile, //-
        DeleteSaveSession, //-
        EnumerateSessions,
        LoadGame, //-
        UploadSaveGame,
        DownloadSaveGame //-
    }
}
