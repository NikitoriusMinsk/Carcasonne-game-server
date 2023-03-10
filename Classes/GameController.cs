using Carcasonne_game_server.Classes.ParsedJson;

namespace Carcasonne_game_server.Classes
{
    public class GameController
    {
        public Player[] Players { get; }
        public Board Board { get; }
        public Tile[] TilePool { get; }

        public GameController()
        {
            Players = new Player[6];
            Board = new Board();
            TilePool = new Tile[1];
        }

        public async Task<Tile[]> GenerateTilePool()
        {
            Tile[] result = await LoadJsonTiles();

            return result.ToArray();
        }

        public static async Task<Tile[]> LoadJsonTiles()
        {
            List<Tile> result = new();
            List<ParsedTile> parsedTiles = await JsonFileReader.ReadFileAsync<List<ParsedTile>>("./TileLists/Carcasonne_Tiles_v1.json");
            foreach (ParsedTile tile in parsedTiles)
            {
                await Console.Out.WriteLineAsync($"id: {tile.id}");
                //result.Add(new Tile(tile));
            }

            return result.ToArray();
        }
    }
}
