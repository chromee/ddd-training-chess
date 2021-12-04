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

        public GamePresenter Create(CreateGameUseCase createGameUseCase, Game game) => new(game, createGameUseCase, _gameResultView);
    }
}
