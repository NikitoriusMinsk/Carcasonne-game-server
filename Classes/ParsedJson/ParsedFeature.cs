using Carcasonne_game_server.Classes.Features;

namespace Carcasonne_game_server.Classes.ParsedJson
{
    public class ParsedFeature
    {
        public string type { get; set; }
        public BonusPoints bonusPoints { get; set; }
        public string[] connectsTo { get; set; }
    }
}
