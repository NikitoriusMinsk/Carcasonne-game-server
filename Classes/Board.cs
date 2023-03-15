using Carcasonne_game_server.Classes.Meeples;
using System.Drawing;

namespace Carcasonne_game_server.Classes
{
    public class Board
    {
        public Tile[,] Tiles { get; }

        public Board()
        {
            Tiles = new Tile[145, 145];
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
            //TODO: rotate the tile before placement
            Tiles[pos.X, pos.Y] = tile;

            return Tiles;
        }

        public Point[] GetPossiblePlacements(Tile tile)
        {
            List<Point> result = new();

            for (int i = 0; i < Tiles.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < Tiles.GetLength(1) - 1; j++)
                {
                    if (Tiles[i, j] != null) continue;

                    if ((Tiles[CheckBoundries(i - 1), j] == null) &&
                        (Tiles[i, CheckBoundries(j + 1)] == null) &&
                        (Tiles[CheckBoundries(i + 1), j] == null) &&
                        (Tiles[i, CheckBoundries(j - 1)] == null)
                    ) continue;

                    if (tile.CanBePlaced(
                        Tiles[CheckBoundries(i - 1), j],
                        Tiles[i, CheckBoundries(j + 1)],
                        Tiles[CheckBoundries(i + 1), j],
                        Tiles[i, CheckBoundries(j - 1)])
                    ) result.Add(new Point(i, j));

                }
            }

            return result.ToArray();
        }

        private int CheckBoundries(int x)
        {
            if (x < 0) return 0;
            if ((x > Tiles.GetLength(0) - 1)) return Tiles.GetLength(0) - 1;
            if ((x > Tiles.GetLength(1) - 1)) return Tiles.GetLength(1) - 1;
            return x;
        }

        public void PlaceMeeple(Meeple meeple, Point pos, TilePlaces place)
        {
            Tiles[pos.X, pos.Y][place].Owner = meeple;
        }
    }
}
