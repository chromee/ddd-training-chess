using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Applications.Games
{
    public class GamePresenterFactory
    {
        private readonly GameUseCase _gameUseCase;
        private readonly IGameResultView _gameResultView;

        public GamePresenterFactory(
            // GameUseCase gameUseCase, 
            IGameResultView gameResultView)
        {
            // _gameUseCase = gameUseCase;
            _gameResultView = gameResultView;
        }

        // TODO: GameUseCase と循環参照になってしまうため仕方なく Create 時に GameUseCase をとっているが、もうちょっと綺麗にしたい
        public GamePresenter Create(GameUseCase gameUseCase, Game game) => new GamePresenter(gameUseCase, _gameResultView, game);
    }
}
