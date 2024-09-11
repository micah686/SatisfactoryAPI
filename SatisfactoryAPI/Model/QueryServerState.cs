using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model
{
    public class QueryServerState
    {
        public ServerGameState serverGameState { get; set; }
    }

    public class ServerGameState
    {
        public string activeSessionName { get; set; }
        public int numConnectedPlayers { get; set; }
        public int playerLimit { get; set; }
        public int techTier { get; set; }
        public string activeSchematic { get; set; }
        public string gamePhase { get; set; }
        public bool isGameRunning { get; set; }
        public int totalGameDuration { get; set; }
        public bool isGamePaused { get; set; }
        public float averageTickRate { get; set; }
        public string autoLoadSessionName { get; set; }
    }
}
