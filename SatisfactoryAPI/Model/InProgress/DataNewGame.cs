﻿namespace SatisfactoryAPI.Model.InProgress;

public class DataNewGame
{
    public ServerNewGameData NewGameData { get; set; }
}
public class ServerNewGameData
{
    public string SessionName { get; set; }
    public string MapName { get; set; }
    public string StartingLocation { get; set; }
    public bool SkipOnboarding { get; } = true;
    public Dictionary<string, string> AdvancedGameSettings { get; set; }
    public Dictionary<string, string> CustomOptionsOnlyForModding { get; set; }
}