using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalRChat.Hubs;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TugOfSignalWaR
{
    public class GameStateBackgroundService : BackgroundService
    {
        private readonly IHubContext<GameHub> hubContext;
        public GameStateBackgroundService(IHubContext<GameHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GameState.Instance.StartTime = DateTime.Now;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now - GameState.Instance.StartTime > TimeSpan.FromMinutes(1))
                {
                    await hubContext.Clients.All.SendAsync("GameOver", GameState.Instance);
                    await Task.Delay(5000);
                    GameState.Instance.StartTime = DateTime.Now;
                }

                await hubContext.Clients.All.SendAsync("GameUpdated", GameState.Instance);
                await Task.Delay(500);
            }
        }
    }
}