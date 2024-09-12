using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Endpoints.GetServerState
{
    public class ServerStateResponse
    {
        public GameState ServerGameState { get; set; }

        public class GameState
        {
            public string ActiveSessionName { get; set; }
            public int NumConnectedPlayers { get; set; }
            public int PlayerLimit { get; set; }
            public int TechTier { get; set; }
            public string ActiveSchematic { get; set; }
            public string GamePhase { get; set; }
            public bool IsGameRunning { get; set; }
            public int TotalGameDuration { get; set; }
            public bool IsGamePaused { get; set; }
            public float AverageTickRate { get; set; }
            public string AutoLoadSessionName { get; set; }
        }
    }

    
}
