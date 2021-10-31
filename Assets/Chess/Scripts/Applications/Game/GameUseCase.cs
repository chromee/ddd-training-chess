namespace Chess.Scripts.Applications.Game
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
