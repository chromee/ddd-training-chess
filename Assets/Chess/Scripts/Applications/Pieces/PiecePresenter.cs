using System;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Applications.Pieces
{
    public class PiecePresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public PiecePresenter(Piece piece, IPieceView view)
        {
            piece.PositionAsObservable
                .Subscribe(position =>
                {
                    view.SetPosition(position.ToVector2());
                })
                .AddTo(_disposable);

            piece.IsDeadAsObservable
                .Where(isDead => isDead)
                .Subscribe(_ =>
                {
                    view.Dead();
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
