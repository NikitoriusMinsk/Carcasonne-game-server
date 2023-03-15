using Carcasonne_game_server.Classes;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace Carcasonne_game_server.Hubs
{

    /*
        --- List of hub events ---
        Server-sent:
            RoomsUpdated
            PlayerLeftRoom
            TileDrawn
            TilePlaced
        Client-sent:
            CreateRoom
            DeleteRoom
            JoinRoom
            LeaveRoom
            StartGame
            PlaceTile
            GetPossiblePlacements
     */
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

        public async Task<string[]> CreateRoom(string roomName, string playerName)
        {
            if (Rooms.Where(room => room.Name == roomName).Any())
            {
                return Rooms.Select(room => room.Name).ToArray();
            }

            Rooms.Add(new GameRoom(roomName, new Player(Context.ConnectionId, playerName)));
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"Room {roomName} created");
            await Clients.AllExcept(Context.ConnectionId).SendAsync("RoomsUpdated", Rooms.Select(room => room.Name).ToArray());
            return Rooms.Select(room => room.Name).ToArray();
        }

        public async Task<string[]> DeleteRoom(string roomName)
        {
            GameRoom roomToDelete = Rooms.Where(room => room.Name == roomName).First();
            roomToDelete.Players.ForEach(player => Groups.RemoveFromGroupAsync(player.Id, roomName));
            Rooms.Remove(roomToDelete);
            await Clients.AllExcept(Context.ConnectionId).SendAsync("RoomsUpdated", Rooms.Select(room => room.Name).ToArray());
            return Rooms.Select(room => room.Name).ToArray();
        }

        public async Task<RoomInfo> JoinRoom(string roomName, string playerName)
        {
            GameRoom room = Rooms.Where(room => room.Name == roomName).First();
            room.Players.Add(new Player(Context.ConnectionId, playerName));
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"User {Context.ConnectionId} joined room {roomName}");
            return new RoomInfo(room.Name, room.Players.Select(player => player.Name).ToArray());
        }

        public async Task LeaveRoom(string roomName)
        {
            GameRoom room = Rooms.Where(room => room.Name == roomName).First();
            room.Players.RemoveAll(player => player.Id == Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            Logger.LogInformation($"User {Context.ConnectionId} left room {roomName}");
            // Send a message to the room to notify other room members
            await Clients.Group(roomName).SendAsync("PlayerLeftRoom", new RoomInfo(room.Name, room.Players.Select(player => player.Name).ToArray()));
        }

        public async Task StartGame()
        {
            GameRoom room = Rooms.Where(room => room.Players.Any(player => player.Id == Context.ConnectionId)).First();
            room.StartGame();
            await Clients.Client(room.Players[room.CurrentPlayer].Id).SendAsync("TileDrawn", room.TilePool[0].Id);
        }

        public async Task PlaceTile(string id, int row, int column, int rotation)
        {
            GameRoom room = Rooms.Where(room => room.Players.Any(player => player.Id == Context.ConnectionId)).First();
            room.PlaceTile(id, new Point(row, column), (TileRotation)rotation);
            await Clients.Group(room.Name).SendAsync("TilePlaced", id, row, column, rotation);
            room.NextPlayer();
            await Clients.Client(room.Players[room.CurrentPlayer].Id).SendAsync("TileDrawn", room.TilePool[0].Id);
        }

        public Point[] GetPossiblePlacements(string id, int rotation)
        {
            GameRoom room = Rooms.Where(room => room.Players.Any(player => player.Id == Context.ConnectionId)).First();
            Tile tileToPlace = room.TilePool.First(t => t.Id == id).Rotate((TileRotation)rotation);
            return room.Board.GetPossiblePlacements(tileToPlace);
        }

    }

    public class RoomInfo
    {
        public string Name { get; set; }
        public string[] Players { get; set; }

        public RoomInfo(string name, string[] players)
        {
            Name = name;
            Players = players;
        }
    }
}
