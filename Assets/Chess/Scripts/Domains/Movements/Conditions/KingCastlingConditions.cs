using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class KingCastlingConditions : IMoveConditions
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            if (!piece.IsType(PieceType.King)) return false;

            var isWhite = piece.Color == PlayerColor.White;
            var y = isWhite ? PieceConstants.WhiteYLine : PieceConstants.BlackYLine;

            // 初期位置でなければ移動できない
            if (piece.Position.X != PieceConstants.KingX || piece.Position.Y != y) return false;

            // 初期位置yラインでなければ移動できない
            if (destination.Y != y) return false;

            // 移動方向にルークがなければ移動できない
            var isRightMove = destination.X - piece.Position.X > 0;
            var rook = game.Board.GetPiece(new Position(isRightMove ? 7 : 0, y));
            if (rook == null || rook.IsOpponent(piece) || !rook.IsType(PieceType.Rook)) return false;

            // ルークとの間にコマがあると移動できない
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

            // 移動した結果、チェックになる場合は移動できない
            var cloneGame = game.Clone();
            var cloneKing = cloneGame.Board.GetPiece(piece.Position);
            cloneGame.Board.MovePiece(cloneKing.Position, destination);
            if (cloneGame.StatusHandler.CanPick(cloneKing)) return false;

            return true;
        }
    }
}
