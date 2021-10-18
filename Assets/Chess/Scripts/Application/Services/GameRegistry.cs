using Chess.Domain.Games;

namespace Chess.Application.Services
{
    public class GameRegistry
    {
        public Game CurrentGame { get; private set; }
        public void Register(Game game) => CurrentGame = game;
    }
}
