using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;
using UniRx;
using UnityEngine;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class PromotionNotifier : IDisposable
    {
        private readonly ReactiveProperty<Position> _targetPawnPosition = new();
        public IObservable<Position> OnPawnPromotion => _targetPawnPosition;

        internal Position TargetPawnPosition => _targetPawnPosition.Value;

        internal void PawnPromotion(Position position)
        {
            _targetPawnPosition.Value = position;
        }

        public void Dispose()
        {
            _targetPawnPosition.Dispose();
        }
    }
}
