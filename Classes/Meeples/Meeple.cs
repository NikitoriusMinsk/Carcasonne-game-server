namespace Carcasonne_game_server.Classes.Meeples
{
    public abstract class Meeple
    {
        public string Name { get; }
        public string Description { get; }
        public int Weight { get; }
        public Player Owner { get; }

        protected Meeple(string name, string description, int weight, Player owner)
        {
            Name = name;
            Description = description;
            Weight = weight;
            Owner = owner;
        }
    }
}
