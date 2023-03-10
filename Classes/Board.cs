namespace Carcasonne_game_server.Classes
{
    public class Board
    {
        public Tile[,] Tiles;

        public Board(Tile[,] tiles)
        {
            Tiles = tiles;
        }

        public Tile this[int i, int j]
        {
            get
            {
                return Tiles[i, j];
            }
            set
            {
                Tiles[i, j] = value;
            }
        }
    }
}
