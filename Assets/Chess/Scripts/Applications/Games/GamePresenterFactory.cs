using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Applications.Games
{
    public class GamePresenterFactory
    {
        private readonly IGameResultView _gameResultView;

        public GamePresenterFactory(IGameResultView gameResultView)
        {
            _gameResultView = gameResultView;
        }

        // TODO: GameUseCase と循環参照になってしまうため仕方なく Create 時に GameUseCase をとっているが、もうちょっと綺麗にしたい
        public GamePresenter Create(GameUseCase gameUseCase, Game game) => new(game, gameUseCase, _gameResultView);
    }
}
