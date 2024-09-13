namespace SatisfactoryAPI.Model.Endpoints.UploadSave;

public class UploadSaveGameRequest
{
    public string SaveName { get; set; }
    public bool LoadSaveGame { get; set; }
    public bool EnableAdvancedGameSettings { get; set; }
}