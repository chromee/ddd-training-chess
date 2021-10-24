using System.Linq;
using Chess.Domain.Boards;
using Chess.Domain.Pieces;

namespace Chess.Domain.Games
{
    public class CheckService
    {
        private readonly PieceService _pieceService;

        public CheckService(PieceService pieceService)
        {
            _pieceService = pieceService;
        }

        public bool IsCheck(Board board, Player turnPlayer, Player opponentPlayer)
        {
            var opponentKing = board.GetPiece(opponentPlayer, PieceType.King);
            var myPieces = board.GetPieces(turnPlayer);
            var myPiecesDestinations = myPieces.SelectMany(piece => _pieceService.MoveCandidates(piece, board));
            return myPiecesDestinations.Any(pos => pos == opponentKing.Position);
        }
    }
}
