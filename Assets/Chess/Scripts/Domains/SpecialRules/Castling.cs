using System;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class Castling : SpecialRule
    {
        public override void TryExecute(Board board)
        {
            var lastHand = board.LastPieceMovement;
            if (lastHand == null) return;

            var king = lastHand.MovedPiece;
            if (!king.IsType(PieceType.King)) return;

            var movement = lastHand.NextPosition - lastHand.PrevPosition;
            if (Math.Abs(movement.X) != 2) return;

            var isRightMove = movement.X > 0;
            var y = king.Color == PlayerColor.White ? 0 : 7;
            var rook = board.GetPiece(new Position(isRightMove ? 7 : 0, y));

            if (rook == null || rook.IsOpponent(king) || !rook.IsType(PieceType.Rook)) return;

            var x = king.Position.X + (isRightMove ? -1 : 1);
            board.MovePiece(rook.Position, new Position(x, y));
        }
    }
}
