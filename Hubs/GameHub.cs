using Carcasonne_game_server.Classes;
using Microsoft.AspNetCore.SignalR;

namespace Carcasonne_game_server.Hubs
{
    public class GameHub : Hub
    {
        private static List<GameRoom> Rooms { get; } = new List<GameRoom>();
        private readonly ILogger<GameHub> Logger;

        public GameHub(ILogger<GameHub> logger)
        {
            Logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            Logger.LogInformation($"User {Context.ConnectionId} connected to GameHub");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Logger.LogInformation($"User {Context.ConnectionId} disconnected from GameHub");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateRoom(string roomName)
        {
            Rooms.Add(new GameRoom(roomName, new Player(Context.ConnectionId, Context.User.Identity.Name)));
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"Room {roomName} created");
            await Clients.All.SendAsync("RoomsUpdated", Rooms.Select(room => room.Name).ToArray());
        }

        public async Task DeleteRoom(string roomName)
        {
            GameRoom roomToDelete = Rooms.Where(room => room.Name == roomName).First();
            roomToDelete.Players.ForEach(player => Groups.RemoveFromGroupAsync(player.Id, roomName));
            Rooms.Remove(roomToDelete);
            await Clients.All.SendAsync("RoomsUpdated", Rooms.Select(room => room.Name).ToArray());
        }

        public async Task JoinRoom(string roomName)
        {
            Rooms.Where(room => room.Name == roomName).First().Players.Add(new Player(Context.ConnectionId, Context.User.Identity.Name));
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"User {Context.ConnectionId} joined room {roomName}");
        }

        public async Task LeaveRoom(string roomName)
        {
            Rooms.Where(room => room.Name == roomName).First().Players.RemoveAll(player => player.Id == Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"User {Context.ConnectionId} left room {roomName}");
        }

    }
}
