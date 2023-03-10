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

        public bool CanBePlaced(Tile top, Tile right, Tile bottom, Tile left)
        {
            bool topCompatible = top is null || top.Bottom == Top;
            bool rightCompatible = right is null || right.Left == Right;
            bool bottomCompatible = bottom is null || bottom.Top == Bottom;
            bool leftCompatible = left is null || left.Right == Left;

            return topCompatible && rightCompatible && bottomCompatible && leftCompatible;
        }

        public Feature this[TilePlaces place]
        {
            get
            {
                switch (place)
                {
                    case TilePlaces.Left:
                        return Left;
                    case TilePlaces.Right:
                        return Right;
                    case TilePlaces.Top:
                        return Top;
                    case TilePlaces.Bottom:
                        return Bottom;
                    case TilePlaces.Center:
                        return Center;
                    default: return null;
                }
            }
        }

        public override string ToString()
        {
            return $"\t | {Top} | \t\n{Left} | {Center} | {Right}\n\t | {Bottom} | \t";
        }
    }

    public enum TilePlaces
    {
        Top,
        Bottom,
        Left,
        Right,
        Center
    }
}
