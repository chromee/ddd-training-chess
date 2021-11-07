using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class SpecialRuleService
    {
        private readonly PromotionNotifier _promotionNotifier;
        private readonly PieceFactory _pieceFactory;

        public SpecialRuleService(PromotionNotifier promotionNotifier, PieceFactory pieceFactory)
        {
            _promotionNotifier = promotionNotifier;
            _pieceFactory = pieceFactory;
        }

        public void Promotion(Board board, PieceType type)
        {
            var targetPawn = _promotionNotifier.TargetPawn;

            if (targetPawn == null) throw new Exception("プロモーションできるポーンが存在しません。");
            if (type is PieceType.King or PieceType.Pawn) throw new ArgumentException($"{type} にはなれません。");

            targetPawn.Die();
            board.RemovePiece(targetPawn);

            var newPiece = type switch
            {
                PieceType.Knight => _pieceFactory.CreateKnight(targetPawn.Owner, targetPawn.Position),
                PieceType.Bishop => _pieceFactory.CreateBishop(targetPawn.Owner, targetPawn.Position),
                PieceType.Rook => _pieceFactory.CreateRook(targetPawn.Owner, targetPawn.Position),
                PieceType.Queen => _pieceFactory.CreateQueen(targetPawn.Owner, targetPawn.Position),
                _ => throw new ArgumentException(),
            };

            board.AddPiece(newPiece);
        }
    }
}
