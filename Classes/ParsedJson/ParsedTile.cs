namespace Carcasonne_game_server.Classes.ParsedJson
{
    public class ParsedTile
    {
        public string id { get; set; }
        public ParsedFeature top { get; set; }
        public ParsedFeature bottom { get; set; }
        public ParsedFeature left { get; set; }
        public ParsedFeature right { get; set; }
        public ParsedFeature center { get; set; }
    }
}
