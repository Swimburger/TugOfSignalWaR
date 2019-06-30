using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models;
using System;
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

        public override Task OnConnectedAsync()
        {
            GameState.AmountOfPlayers++;
            if (GameState.BlueTeamSize > GameState.RedTeamSize)
            {
                Context.Items[TeamKey] = RedTeam;
                GameState.RedTeamSize++;
            }
            else
            {
                Context.Items[TeamKey] = BlueTeam;
                GameState.BlueTeamSize++;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            GameState.AmountOfPlayers--;
            if (string.Equals(Context.Items[TeamKey], BlueTeam))
            {
                GameState.BlueTeamSize--;
            }
            else
            {
                GameState.RedTeamSize--;
            }

            return base.OnDisconnectedAsync(exception);
        }

        public JoinGameResponse JoinGame()
        {
            return new JoinGameResponse { Team = Context.Items[TeamKey] as string, GameState = GameState };
        }

        public void Pull()
        {
            if (string.Equals(Context.Items[TeamKey], BlueTeam))
            {
                GameState.FlagPosition++;
            }
            else
            {
                GameState.FlagPosition--;
            }
        }
    }
}
