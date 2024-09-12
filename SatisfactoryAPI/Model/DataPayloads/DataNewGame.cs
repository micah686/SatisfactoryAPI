using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.DataPayloads
{
    public class DataNewGame
    {
        public ServerNewGameData NewGameData { get; set; }
    }
    public class ServerNewGameData
    {
        public string SessionName { get; set; }
        public string MapName { get; set; }
        public string StartingLocation { get; set; }
        public bool bSkipOnboarding { get; set; }
        public Dictionary<string, string> AdvancedGameSettings { get; set; }
        public Dictionary<string, string> CustomOptionsOnlyForModding { get; set; }
    }
}
