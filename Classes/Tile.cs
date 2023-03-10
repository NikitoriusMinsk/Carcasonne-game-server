using Carcasonne_game_server.Classes.Features;

namespace Carcasonne_game_server.Classes
{
    public class Tile
    {
        public string Id { get; }
        public Feature Top { get; }
        public Feature Bottom { get; }
        public Feature Left { get; }
        public Feature Right { get; }
        public Feature Center { get; }

        public Tile(string id, Feature top, Feature bottom, Feature left, Feature right, Feature center)
        {
            Id = id;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
            Center = center;
        }
    }
}
