using Carcasonne_game_server.Classes.Meeples;

namespace Carcasonne_game_server.Classes
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        public Meeple[] Meeples { get; set; }

        public int Score { get; set; }

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
            Meeples = new Meeple[6];
        }
    }
}
