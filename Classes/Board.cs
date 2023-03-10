using Carcasonne_game_server.Classes.Meeples;
using System.Drawing;

namespace Carcasonne_game_server.Classes
{
    public class Board
    {
        public Tile[,] Tiles { get; }

        public Board()
        {
            Tiles = new Tile[144, 144];
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

        public Tile[,] PlaceTile(Tile tile, Point pos)
        {
            Tiles[pos.X, pos.Y] = tile;

            return Tiles;
        }

        public Point[] GetPossiblePlacements(Tile tile)
        {
            List<Point> result = new();

            for (int i = 0; i < Tiles.GetLength(1); i++)
            {
                for (int j = 0; j < Tiles.GetLength(2); j++)
                {
                    if (Tiles[i, j] != null) continue;
                    if (tile.CanBePlaced(Tiles[i - 1, j], Tiles[i, j + 1], Tiles[i + 1, j], Tiles[i, j - 1]))
                        result.Add(new Point(i, j));
                }
            }

            return result.ToArray();
        }

        public void PlaceMeeple(Meeple meeple, Point pos, TilePlaces place)
        {
            Tiles[pos.X, pos.Y][place].Owner = meeple;
        }
    }
}
