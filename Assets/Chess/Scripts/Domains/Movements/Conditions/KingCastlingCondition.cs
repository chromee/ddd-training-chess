using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class KingCastlingCondition : IMovementCondition
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            if (!piece.IsType(PieceType.King)) return false;

            var y = piece.IsWhite() ? PieceConstants.WhiteYLine : PieceConstants.BlackYLine;

            // 初期位置でなければ実行できない
            if (piece.Position.X != PieceConstants.KingX || piece.Position.Y != y) return false;

            // 初期位置yラインでなければ実行できない
            if (destination.Y != y) return false;

            // 移動方向にルークがなければ実行できない
            var isRightMove = destination.X - piece.Position.X > 0;
            var rook = game.Board.GetPiece(new Position(isRightMove ? 7 : 0, y));
            if (rook == null || rook.IsOpponent(piece) || !rook.IsType(PieceType.Rook)) return false;

            // ルークとの間にコマがあると実行できない
            if (isRightMove)
            {
                for (var i = piece.Position.X + 1; i < 7; i++)
                {
                    if (game.Board.GetPiece(new Position(i, y)) != null) return false;
                }
            }
            else
            {
                for (var i = piece.Position.X - 1; i > 0; i--)
                {
                    if (game.Board.GetPiece(new Position(i, y)) != null) return false;
                }
            }

            // 移動した結果、チェックになる場合は実行できない
            var cloneGame = game.Clone();
            var cloneKing = cloneGame.Board.GetPiece(piece.Position);
            cloneGame.Board.MovePiece(cloneKing.Position, destination);
            if (cloneGame.BoardStatusSolver.CanPick(cloneKing)) return false;

            return true;
        }
    }
}
