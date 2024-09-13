using System.Text.Json.Serialization;

namespace SatisfactoryAPI.Model.Endpoints.CreateNewGame;

public class ServerNewGameData
{
    public string SessionName { get; set; }
    public string MapName { get; set; }
    public string StartingLocation { get; set; }
    [Obsolete("Property was named incorrectly on the dedicated server backend. This will change once it's been fixed")]
    [JsonPropertyName("bSkipOnboarding")]
    public bool SkipOnboarding { get; set; }
    public Dictionary<string, string> AdvancedGameSettings { get; set; }
    public Dictionary<string, string> CustomOptionsOnlyForModding { get; set; }
}

public class CreateNewGamePayload
{
    public ServerNewGameData NewGameData { get; set; }
}