using Carcasonne_game_server.Classes.Meeples;

namespace Carcasonne_game_server.Classes
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        //the connection will be a websocket in the future
        public string Connection { get; }
        public Meeple[] Meeples { get; set; }

        public int Score { get; set; }

        public Player(int id, string name, string connection)
        {
            Id = id;
            Name = name;
            Connection = connection;
            Meeples = new Meeple[6];
        }
    }
}
