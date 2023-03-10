namespace Carcasonne_game_server.Classes.Features
{
    public class MonasteryFeature : Feature
    {

        public MonasteryFeature(Feature[] connectsTo, BonusPoints bonusPoints) : base(connectsTo, bonusPoints) { }


        public override int GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
