using System;
using Chess.Scripts.Domains.Games;
using UniRx;

namespace Chess.Scripts.Applications.Games
{
    public class GamePresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public GamePresenter(GameUseCase gameUseCase, IGameResultView gameResultView, Game game)
        {
            game.StatusHandler.CurrentStatusObservable
                .Subscribe(status =>
                {
                    switch (status)
                    {
                        case GameStatus.Check:
                            gameResultView.ShowCheck();
                            break;
                        case GameStatus.Checkmate:
                            gameResultView.ShowCheckmate();
                            break;
                        case GameStatus.Stalemate:
                            gameResultView.ShowStalemate();
                            break;
                        default:
                            gameResultView.HideAll();
                            break;
                    }
                }).AddTo(_disposable);

            gameResultView.OnRestart
                .Subscribe(_ => gameUseCase.CreateGame())
                .AddTo(_disposable);

            gameResultView.HideAll();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
