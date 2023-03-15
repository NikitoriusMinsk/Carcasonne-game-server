using Carcasonne_game_server.Classes.Features;
using Carcasonne_game_server.Classes.ParsedJson;

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

        public Tile(string id, Feature top, Feature right, Feature bottom, Feature left, Feature center)
        {
            Id = id;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
            Center = center;
        }

        public Tile(ParsedTile tile)
        {
            Id = tile.id;
            Top = DecideFeatureType(tile.top);
            Right = DecideFeatureType(tile.right);
            Bottom = DecideFeatureType(tile.bottom);
            Left = DecideFeatureType(tile.left);
            Center = DecideFeatureType(tile.center);
        }

        private Feature DecideFeatureType(ParsedFeature feature)
        {
            List<Feature> _connectsTo = new();

            foreach (string place in feature.connectsTo)
            {
                switch (place)
                {
                    case "top":
                        _connectsTo.Add(Top);
                        break;
                    case "bottom":
                        _connectsTo.Add(Bottom);
                        break;
                    case "left":
                        _connectsTo.Add(Left);
                        break;
                    case "right":
                        _connectsTo.Add(Right);
                        break;
                    case "center":
                        _connectsTo.Add(Center);
                        break;
                }
            }

            switch (feature.type)
            {
                case "city":
                    return new CityFeature(_connectsTo.ToArray(), feature.bonusPoints);
                case "monastery":
                    return new MonasteryFeature(_connectsTo.ToArray(), feature.bonusPoints);
                case "field":
                    return new FieldFeature(_connectsTo.ToArray(), feature.bonusPoints);
                case "road":
                    return new RoadFeature(_connectsTo.ToArray(), feature.bonusPoints);
                default: return null;
            }
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
            return $"{Id}\n  | {Top} |  \n---------\n{Left} | {Center} | {Right}\n---------\n  | {Bottom} |  \n";
        }

        public Tile Rotate(TileRotation rotation)
        {
            switch (rotation)
            {
                case TileRotation._0deg: return this;
                case TileRotation._90deg: return new Tile(Id, Left, Top, Right, Bottom, Center);
                case TileRotation._180deg: return new Tile(Id, Bottom, Left, Top, Right, Center);
                case TileRotation._270deg: return new Tile(Id, Right, Bottom, Left, Top, Center);

                default:
                    return this;
            }
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

    public enum TileRotation
    {
        _0deg = 0,
        _90deg = 1,
        _180deg = 2,
        _270deg = 3
    }
}
