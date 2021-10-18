using System;
using System.Linq;
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

        public void Move(Piece piece, Board board, Position destination, PlayerColor thisTurnPlayer)
        {
            // ピースがボードになかったとき
            if (!board.HasPiece(piece))
                throw new ArgumentException("the piece is not on board.");

            if (!HasMoveCandidate(piece, board, destination))
                throw new ArgumentException("the piece cannot move this position.");

            if (IsSuicideMove(piece, board, destination, thisTurnPlayer))
                throw new ArgumentException("this movement is suicide.");

            var destPiece = board.GetPiece(destination);
            if (destPiece != null && !destPiece.IsOpponent(piece))
                throw new ArgumentException("the piece cannot kill allies pieces.");

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
        private bool IsSuicideMove(Piece piece, Board board, Position destination, PlayerColor turnPlayerColor)
        {
            var cloneBoard = board.Clone();
            var clonePiece = cloneBoard.GetPiece(piece.Position);
            ForceMove(clonePiece, cloneBoard, destination);
            return _checkService.IsCheck(cloneBoard, turnPlayerColor.Opponent());
        }
    }
}
