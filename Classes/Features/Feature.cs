using Carcasonne_game_server.Classes.Meeples;

namespace Carcasonne_game_server.Classes.Features
{
    public abstract class Feature
    {
        public abstract string Type { get; }

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
            _connectsTo = connectsTo;
            BonusPoints = bonusPoints;
            _bonusPoints = bonusPoints;
            Owner = null;
        }

        public abstract int GetValue();

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Feature a, Feature b)
        {
            return a.Type == b.Type;
        }

        public static bool operator !=(Feature a, Feature b)
        {
            return a.Type != b.Type;
        }

    }

}
