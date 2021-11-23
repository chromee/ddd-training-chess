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

        public GamePresenter Create(GameUseCase gameUseCase, Game game) => new(game, gameUseCase, _gameResultView);
    }
}
