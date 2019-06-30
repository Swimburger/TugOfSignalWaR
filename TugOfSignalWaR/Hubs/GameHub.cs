using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models;
using System.Threading.Tasks;
using TugOfSignalWaR.Models;

namespace SignalRChat.Hubs
{
    public class GameHub : Hub
    {
        private const string TeamKey = "team";
        private const string RedTeam = "red";
        private const string BlueTeam = "blue";
        public GameState GameState => GameState.Instance;

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public JoinGameResponse JoinGame()
        {
            GameState.AmountOfPlayers++;
            if (GameState.BlueTeamSize > GameState.RedTeamSize)
            {
                Context.Items[TeamKey] = RedTeam;
                GameState.RedTeamSize++;
                return new JoinGameResponse { Team = RedTeam, GameState = GameState };
            }
            else
            {
                Context.Items[TeamKey] = BlueTeam;
                GameState.BlueTeamSize++;
                return new JoinGameResponse { Team = BlueTeam, GameState = GameState };
            }
        }
    }
}
