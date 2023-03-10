namespace Carcasonne_game_server.Classes.Features
{
    public class RoadFeature : Feature
    {
        public override string Type => "road";
        public RoadFeature(Feature[] connectsTo, BonusPoints bonusPoints) : base(connectsTo, bonusPoints) { }


        public override int GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
