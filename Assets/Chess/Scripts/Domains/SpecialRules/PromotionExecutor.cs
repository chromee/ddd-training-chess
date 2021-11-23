using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class PromotionExecutor
    {
        private readonly PieceFactory _pieceFactory;
        private readonly PromotionNotifier _promotionNotifier;

        public PromotionExecutor(PromotionNotifier promotionNotifier, PieceFactory pieceFactory)
        {
            _promotionNotifier = promotionNotifier;
            _pieceFactory = pieceFactory;
        }

        public void Promotion(Board board, PieceType type)
        {
            var targetPawn = board.GetPiece(_promotionNotifier.TargetPawnPosition);

            if (targetPawn == null) throw new Exception("プロモーションできるポーンが存在しません。");
            if (!targetPawn.IsType(PieceType.Pawn)) throw new Exception("ポーンではありません。");
            if (type is PieceType.King or PieceType.Pawn) throw new ArgumentException($"{type} にはなれません。");

            board.RemovePiece(targetPawn);

            var newPiece = type switch
            {
                PieceType.Knight => _pieceFactory.CreateKnight(targetPawn.Color, targetPawn.Position),
                PieceType.Bishop => _pieceFactory.CreateBishop(targetPawn.Color, targetPawn.Position),
                PieceType.Rook => _pieceFactory.CreateRook(targetPawn.Color, targetPawn.Position),
                PieceType.Queen => _pieceFactory.CreateQueen(targetPawn.Color, targetPawn.Position),
                _ => throw new ArgumentException(),
            };

            board.AddPiece(newPiece);
        }
    }
}
