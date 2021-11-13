using VContainer.Unity;

namespace Chess.Scripts.Applications.Games
{
    public class GameInitializer : IInitializable
    {
        private readonly GameUseCase _gameUseCase;

        public GameInitializer(GameUseCase gameUseCase)
        {
            _gameUseCase = gameUseCase;
        }

        public void Initialize()
        {
            _gameUseCase.CreateGame();
        }
    }
}
