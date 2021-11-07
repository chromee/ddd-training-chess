using System;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class PromotionNotifier : IDisposable
    {
        private readonly ReactiveProperty<Piece> _targetPawn = new();
        public IObservable<Piece> OnPawnPromotion => _targetPawn;

        internal Piece TargetPawn => _targetPawn.Value;

        internal void PawnPromotion(Piece pawn)
        {
            if (!pawn.IsType(PieceType.Pawn)) throw new ArgumentException();
            _targetPawn.Value = pawn;
        }

        public void Dispose()
        {
            _targetPawn.Dispose();
        }
    }
}
