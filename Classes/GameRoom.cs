using Carcasonne_game_server.Classes.ParsedJson;
using System.Drawing;

namespace Carcasonne_game_server.Classes
{
    public class GameRoom
    {
        public string Name { get; }
        public GameRoomState State { get; set; }
        public List<Player> Players { get; }
        public Board Board { get; }
        public List<Tile> TilePool { get; set; }
        private static readonly Random Random = new();
        public int CurrentPlayer { get; set; } = 0;

        public GameRoom(string name, Player creator)
        {
            Players = new() { creator };
            Board = new();
            TilePool = new List<Tile>(72);
            Name = name;
            State = GameRoomState.InLobby;
        }

        private static List<Tile> GenerateTilePool()
        {
            List<Tile> result = LoadJsonTiles().OrderBy(a => Random.Next()).ToList();

            return result;
        }

        private static Tile[] LoadJsonTiles()
        {
            List<Tile> result = new();
            List<ParsedTile> parsedTiles = JsonFileReader.ReadFileSync<List<ParsedTile>>("./TileLists/Carcasonne_Tiles_v1.json");
            foreach (ParsedTile tile in parsedTiles)
            {
                result.Add(new Tile(tile));
            }

            return result.ToArray();
        }

        public void StartGame()
        {
            State = GameRoomState.Started;
            TilePool = GenerateTilePool();
        }

        public void NextPlayer()
        {
            if (CurrentPlayer == Players.Count - 1)
            {
                CurrentPlayer = 0;
            }
            else
            {
                CurrentPlayer++;
            }
        }

        public void PlaceTile(string id, Point position, int rotation)
        {
            Tile tileToPlace = TilePool.First(t => t.Id == id);
            Board.PlaceTile(tileToPlace, position, rotation);
            TilePool.Remove(tileToPlace);
        }
    }

    public enum GameRoomState
    {
        Started,
        InLobby,
        Ended
    }
}
