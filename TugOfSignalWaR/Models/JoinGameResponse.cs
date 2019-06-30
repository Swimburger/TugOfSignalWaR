using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TugOfSignalWaR.Models
{
    public class JoinGameResponse
    {
        public string Team { get; set; }
        public GameState GameState { get; set; }
    }
}
