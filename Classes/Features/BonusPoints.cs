namespace Carcasonne_game_server.Classes.Features
{
    public class BonusPoints
    {
        public bool Shield { get; }
        public bool Tavern { get; }
        public bool Lake { get; }

        public BonusPoints(bool shield, bool tavern, bool lake)
        {
            Shield = shield;
            Tavern = tavern;
            Lake = lake;
        }
    }
}
