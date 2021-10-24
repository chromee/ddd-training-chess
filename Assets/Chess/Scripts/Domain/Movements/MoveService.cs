using System;
using System.Linq;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Pieces;

namespace Chess.Domain.Movements
{
    public class MoveService
    {
        private readonly PieceService _pieceService;
        private readonly CheckService _checkService;

        public MoveService(PieceService pieceService, CheckService checkService)
        {
            _pieceService = pieceService;
            _checkService = checkService;
        }

        public void Move(Piece piece, Board board, Position destination, Player ownerPlayer, Player opponentPlayer)
        {
            // ピースの持ち主が違ったとき
            if (!piece.IsOwner(ownerPlayer))
                throw new WrongOwnerException($"{piece} is not {ownerPlayer}'s piece");

            // ピースがボードになかったとき
            if (!board.HasPiece(piece))
                throw new PieceNotExistOnBoardException("the piece is not on board.");

            // 移動先候補でなかったとき
            if (!HasMoveCandidate(piece, board, destination))
                throw new PieceCannotMoveException("the piece cannot move this position.");

            // 自殺行動だったとき
            if (IsSuicideMove(piece, board, destination, opponentPlayer, ownerPlayer))
                throw new SuicideMoveException("this movement is suicide.");

            // 移動先が味方駒だったとき
            var destPiece = board.GetPiece(destination);
            if (destPiece != null && !destPiece.IsOpponent(piece))
                throw new TeamKillException("the piece cannot kill allies pieces.");

            board.RemovePiece(destPiece);
            destPiece?.Die();
            piece.Move(destination);
        }

        public void ForceMove(Piece piece, Board board, Position destination)
        {
            var destPiece = board.GetPiece(destination);
            board.RemovePiece(destPiece);
            destPiece?.Die();
            piece.Move(destination);
        }

        private bool HasMoveCandidate(Piece piece, Board board, Position position)
        {
            var candidates = _pieceService.MoveCandidates(piece, board);
            return candidates.Contains(position);
        }

        /// <summary>
        /// 移動した結果、チェックにならないか
        /// </summary>
        private bool IsSuicideMove(Piece piece, Board board, Position destination,
            Player turnPlayer, Player opponentPlayer)
        {
            var cloneBoard = board.Clone();
            var clonePiece = cloneBoard.GetPiece(piece.Position);
            ForceMove(clonePiece, cloneBoard, destination);
            return _checkService.IsCheck(cloneBoard, turnPlayer, opponentPlayer);
        }
    }

    #region 例外

    public class WrongOwnerException : Exception
    {
        public WrongOwnerException(string message) : base(message)
        {
        }
    }

    public class PieceNotExistOnBoardException : Exception
    {
        public PieceNotExistOnBoardException(string message) : base(message)
        {
        }
    }

    public class PieceCannotMoveException : Exception
    {
        public PieceCannotMoveException(string message) : base(message)
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
