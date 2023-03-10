namespace Carcasonne_game_server.Classes.Features
{
    public class FieldFeature : Feature
    {
        public override string Type => "field";
        public FieldFeature(Feature[] connectsTo, BonusPoints bonusPoints) : base(connectsTo, bonusPoints) { }


        public override int GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
