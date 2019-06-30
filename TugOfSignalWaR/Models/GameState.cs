
using System;

namespace SignalRChat.Models
{
    public class GameState
    {
        public static GameState Instance { get; } = new GameState();

        private GameState() { }

        public int AmountOfPlayers { get; set; }
        public int BlueTeamSize { get; set; }
        public int RedTeamSize { get; set; }
        public int FlagPosition { get; set; }
        public DateTime StartTime { get; set; }
    }
}