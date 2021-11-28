using System;
using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Logger;

namespace Chess.Scripts.Domains.Pieces
{
    public class PieceMovementExecutor
    {
        public void Move(Game game, Piece piece, Position destination)
        {
            // そのターンプレイヤーのコマでなかったとき
            if (!piece.IsColor(game.CurrentTurnPlayer))
                throw new WrongPlayerException($"This turn is not {game.NextTurnPlayer}'s turn.");

            // コマがボードになかったとき
            if (!game.Board.HasPiece(piece))
                throw new PieceNotExistOnBoardException("the piece is not on board.");

            // 移動先候補でなかったとき
            if (!CanMoveTo(game, piece, destination))
                throw new OutOfRangePieceMovableRangeException("the piece cannot move this position.");

            // 自殺行動だったとき
            if (IsSuicideMove(game, piece, destination, game.NextTurnPlayer))
                throw new SuicideMoveException("this movement is suicide.");

            var prevPosition = piece.Position;
            var isKillPiece = game.Board.ExistPiece(destination);
            game.Board.MovePiece(prevPosition, destination);
            game.Logger.AddLog(new PieceMovementLog(piece, prevPosition, destination, isKillPiece));

            game.SpecialRuleExecutor.TryExecute(game);

            game.SwapTurn();
        }

        /// <summary>
        ///     指定したコマが指定した位置に移動可能かどうか
        /// </summary>
        private static bool CanMoveTo(Game game, Piece piece, Position destination)
        {
            var destinations = game.PieceMovementSolver.MoveCandidates(piece);
            return destinations.Contains(destination);
        }

        /// <summary>
        ///     移動した結果、チェックにならないか
        /// </summary>
        private bool IsSuicideMove(Game game, Piece piece, Position destination, PlayerColor turnPlayer)
        {
            var cloneGame = game.Clone();
            var clonePiece = cloneGame.Board.GetPiece(piece.Position);
            cloneGame.Board.MovePiece(clonePiece.Position, destination);
            return cloneGame.StatusSolver.IsCheck(turnPlayer);
        }
    }

    #region 例外

    public class WrongPlayerException : Exception
    {
        public WrongPlayerException(string message) : base(message) { }
    }

    public class PieceNotExistOnBoardException : Exception
    {
        public PieceNotExistOnBoardException(string message) : base(message) { }
    }

    public class OutOfRangePieceMovableRangeException : Exception
    {
        public OutOfRangePieceMovableRangeException(string message) : base(message) { }
    }

    public class SuicideMoveException : Exception
    {
        public SuicideMoveException(string message) : base(message) { }
    }

    public class TeamKillException : Exception
    {
        public TeamKillException(string message) : base(message) { }
    }

    #endregion
}
