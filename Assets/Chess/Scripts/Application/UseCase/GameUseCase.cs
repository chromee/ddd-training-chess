using Chess.Application.Services;
using Chess.Domain;
using Chess.Domain.Games;

namespace Chess.Application.UseCase
{
    public class GameUseCase
    {
        public enum GameState { InProgress, Check, Checkmate, }

        private readonly GameRegistry _gameRegistry;

        public GameState CheckGameState()
        {
            var game = _gameRegistry.CurrentGame;
            if (game.IsCheckmate()) return GameState.Checkmate;
            if (game.IsCheck()) return GameState.Check;
            return GameState.InProgress;
        }
    }
}
