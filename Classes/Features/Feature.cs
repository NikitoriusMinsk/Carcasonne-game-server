namespace Carcasonne_game_server.Classes.Features
{
    public abstract class Feature
    {
        private Feature[] _connectsTo;
        public Feature[] ConnectsTo
        {
            get => _connectsTo;
            set => _connectsTo = value;
        }
        private BonusPoints _bonusPoints;
        public BonusPoints BonusPoints
        {
            get => _bonusPoints;
            set => _bonusPoints = value;
        }
        public Meeple? Owner { get; set; }

        protected Feature(Feature[] connectsTo, BonusPoints bonusPoints)
        {
            ConnectsTo = connectsTo;
            BonusPoints = bonusPoints;
            Owner = null;
        }

        public abstract int GetValue();
    }

}
