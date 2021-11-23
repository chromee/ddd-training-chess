using System;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class Castling : ISpecialRule
    {
        public void TryExecute(Game game)
        {
            if (game.Logger.LastPieceMovement == null) return;
            var lastHand = game.Logger.LastPieceMovement.Value;

            if (lastHand.MovedPieceType != PieceType.King) return;

            var movement = lastHand.NextPosition - lastHand.PrevPosition;
            if (Math.Abs(movement.X) != 2) return;

            var isRightMove = movement.X > 0;
            var y = lastHand.MovedPieceColor == PlayerColor.White ? 0 : 7;
            var rook = game.Board.GetPiece(new Position(isRightMove ? 7 : 0, y));

            if (rook == null || !rook.IsColor(lastHand.MovedPieceColor) || !rook.IsType(PieceType.Rook)) return;

            var x = lastHand.NextPosition.X + (isRightMove ? -1 : 1);
            game.Board.MovePiece(rook.Position, new Position(x, y));
        }
    }
}
