using Carcasonne_game_server.Classes.ParsedJson;

namespace Carcasonne_game_server.Classes
{
    public class GameRoom
    {
        public string Name { get; }
        public List<Player> Players { get; set; }
        public Board Board { get; }
        public Tile[] TilePool { get; }

        public GameRoom(string name, Player creator)
        {
            Players = new() { creator };
            Board = new();
            TilePool = GenerateTilePool();
            Name = name;
        }

        public Tile[] GenerateTilePool()
        {
            Tile[] result = LoadJsonTiles();

            return result.ToArray();
        }

        public static Tile[] LoadJsonTiles()
        {
            List<Tile> result = new();
            List<ParsedTile> parsedTiles = JsonFileReader.ReadFileSync<List<ParsedTile>>("./TileLists/Carcasonne_Tiles_v1.json");
            foreach (ParsedTile tile in parsedTiles)
            {
                result.Add(new Tile(tile));
            }

            return result.ToArray();
        }
    }
}
