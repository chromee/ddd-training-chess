using System;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class PromotionNotifier : IDisposable
    {
        private readonly ReactiveProperty<Position> _targetPawnPosition = new();
        public IObservable<Position> OnPawnPromotion => _targetPawnPosition;

        internal Position TargetPawnPosition => _targetPawnPosition.Value;

        public void Dispose()
        {
            _targetPawnPosition.Dispose();
        }

        internal void PawnPromotion(Position position)
        {
            _targetPawnPosition.Value = position;
        }
    }
}
