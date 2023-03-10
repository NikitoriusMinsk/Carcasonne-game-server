using Carcasonne_game_server.Classes;
using Microsoft.AspNetCore.SignalR;

namespace Carcasonne_game_server.Hubs
{
    public class GameHub : Hub
    {
        readonly GameController controller = new();

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task ReadTiles()
        {
            await controller.GenerateTilePool();
        }

    }
}
