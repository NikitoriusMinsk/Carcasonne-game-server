using Microsoft.AspNetCore.SignalR;

namespace Carcasonne_game_server.Hubs
{
    public class GameHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
