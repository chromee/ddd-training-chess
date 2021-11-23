using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Applications.Games
{
    public class GameRegistry
    {
        public Game CurrentGame { get; private set; }
        public void Register(Game game) => CurrentGame = game;
    }
}
