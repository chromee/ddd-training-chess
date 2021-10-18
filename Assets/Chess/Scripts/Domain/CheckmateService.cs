using System;
using System.Linq;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Domain
{
    public class CheckmateService
    {
        private readonly PieceService _pieceService;
        private readonly MoveService _moveService;
        private readonly CheckService _checkService;

        public CheckmateService(PieceService pieceService, MoveService moveService, CheckService checkService)
        {
            _pieceService = pieceService;
            _moveService = moveService;
            _checkService = checkService;
        }

        public bool IsCheckmate(Board board, PlayerColor turnPlayerColor)
        {
            var opponentPlayer = turnPlayerColor.Opponent();
            var opponentKing = board.GetPiece(opponentPlayer, PieceType.King);
            var opponentPieces = board.Pieces.Where(v => v.IsSameColor(opponentPlayer)).ToArray();
            var opponentMoves = opponentPieces.ToDictionary(v => v, v => _pieceService.MoveCandidates(v, board));

            var alliesPieces = board.Pieces.Where(v => v.IsSameColor(turnPlayerColor)).ToArray();
            var alliesMoves = alliesPieces.ToDictionary(v => v, v => _pieceService.MoveCandidates(v, board));
            var checkingPiece = alliesMoves.FirstOrDefault(v => v.Value.Any(pos => pos == opponentKing.Position)).Key;
            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            if (CanAvoid()) return false;

            bool CanAvoid()
            {
                var kindDestinations = opponentMoves[opponentKing];
                var avoidanceFlags = new bool[kindDestinations.Length];
                for (var i = 0; i < kindDestinations.Length; i++)
                {
                    var cloneBoard = board.Clone();
                    var cloneKing = cloneBoard.GetPiece(opponentKing.Position);
                    _moveService.ForceMove(cloneKing, cloneBoard, kindDestinations[i]);
                    avoidanceFlags[i] = !_checkService.IsCheck(cloneBoard, turnPlayerColor);
                }

                return avoidanceFlags.Any(v => v);
            }

            // 他のコマがチェックしてるコマを殺せるかどうか
            if (CanKill()) return false;

            bool CanKill()
            {
                return alliesMoves.Values.Any(v => v.Any(pos => pos == checkingPiece.Position));
            }

            // 他のコマがブロックできるかどうか
            if (CanBlock()) return false;

            bool CanBlock()
            {
                foreach (var opponentPiece in opponentPieces.Where(v => v != opponentKing))
                {
                    var cloneBoard = board.Clone();
                    var clonePiece = cloneBoard.GetPiece(opponentPiece.Position);
                    var pieceMoves = _pieceService.MoveCandidates(clonePiece, cloneBoard);

                    foreach (var destination in pieceMoves)
                    {
                        _moveService.ForceMove(clonePiece, cloneBoard, destination);
                        if (!_checkService.IsCheck(cloneBoard, turnPlayerColor)) return true;
                    }
                }

                return false;
            }

            return true;
        }
    }
}
