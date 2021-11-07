using System;
using Chess.Scripts.Domains.Games;
using UniRx;

namespace Chess.Scripts.Applications.Games
{
    public class GamePresenter : IDisposable
    {
        private readonly IGameResultView _gameResultView;
        private readonly CompositeDisposable _disposable = new();

        public GamePresenter(IGameResultView gameResultView)
        {
            _gameResultView = gameResultView;
        }

        public void Bind(Game game)
        {
            game.GameStatus.Subscribe(status =>
            {
                switch (status)
                {
                    case GameStatus.Check:
                        _gameResultView.ShowCheck();
                        break;
                    case GameStatus.Checkmate:
                        _gameResultView.ShowCheckmate();
                        break;
                    case GameStatus.Stalemate:
                        _gameResultView.ShowStalemate();
                        break;
                    default:
                        _gameResultView.HideAll();
                        break;
                }
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
