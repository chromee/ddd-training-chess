using System;
using System.Linq;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Pieces;

namespace Chess.Domain.Movements
{
    public class MoveService
    {
        public void Move(Piece piece, Position destination, Game game)
        {
            // そのターンプレイヤーのコマでなかったとき
            if (!piece.IsOwner(game.CurrentTurnPlayer))
                throw new WrongPlayerException($"This turn is not {game.NextTurnPlayer}'s turn.");

            // ピースがボードになかったとき
            if (!game.Board.HasPiece(piece))
                throw new PieceNotExistOnBoardException("the piece is not on board.");

            // 移動先候補でなかったとき
            if (!CanMoveTo(piece, game.Board, destination))
                throw new OutOfRangePieceMovableRangeException("the piece cannot move this position.");

            // 自殺行動だったとき
            if (IsSuicideMove(piece, game.Board, destination, game.NextTurnPlayer))
                throw new SuicideMoveException("this movement is suicide.");

            game.Board.MovePiece(piece.Position, destination);
            game.SwapTurn();
        }

        /// <summary>
        /// 指定したコマが指定した位置に移動可能かどうか
        /// </summary>
        private static bool CanMoveTo(Piece piece, Board board, Position destination)
        {
            var destinations = piece.MoveCandidates(board);
            return destinations.Contains(destination);
        }

        /// <summary>
        /// 移動した結果、チェックにならないか
        /// </summary>
        private static bool IsSuicideMove(Piece piece, Board board, Position destination, Player turnPlayer)
        {
            var cloneBoard = board.Clone();
            var clonePiece = cloneBoard.GetPiece(piece.Position);
            cloneBoard.MovePiece(clonePiece.Position, destination);
            return cloneBoard.IsCheck(turnPlayer);
        }
    }

    #region 例外

    public class WrongPlayerException : Exception
    {
        public WrongPlayerException(string message) : base(message)
        {
        }
    }

    public class PieceNotExistOnBoardException : Exception
    {
        public PieceNotExistOnBoardException(string message) : base(message)
        {
        }
    }

    public class OutOfRangePieceMovableRangeException : Exception
    {
        public OutOfRangePieceMovableRangeException(string message) : base(message)
        {
        }
    }

    public class SuicideMoveException : Exception
    {
        public SuicideMoveException(string message) : base(message)
        {
        }
    }

    public class TeamKillException : Exception
    {
        public TeamKillException(string message) : base(message)
        {
        }
    }

    #endregion
}
