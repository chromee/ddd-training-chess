using Chess.Application.Services;
using Chess.Domain;

namespace Chess.Application.UseCase
{
    public class GameUseCase
    {
        public enum GameState { InProgress, Check, Checkmate, }

        private readonly CheckService _checkService;
        private readonly CheckmateService _checkmateService;
        private readonly GameRegistry _gameRegistry;

        public GameState CheckGameState()
        {
            var game = _gameRegistry.CurrentGame;
            if (_checkmateService.IsCheckmate(game.Board, game.CurrentTurnPlayer.Color)) return GameState.Checkmate;
            if (_checkService.IsCheck(game.Board, game.CurrentTurnPlayer.Color)) return GameState.Check;
            return GameState.InProgress;
        }
    }
}
