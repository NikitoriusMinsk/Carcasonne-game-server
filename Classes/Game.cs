namespace Carcasonne_game_server.Classes
{
    public class Game
    {
        public Player[] Players { get; }
        public Board Board { get; }
        public Tile[] TilePool { get; }

        public Game(Player[] players, Board board)
        {
            Players = players;
            Board = board;
            TilePool = new Tile[72];
        }
    }
}
