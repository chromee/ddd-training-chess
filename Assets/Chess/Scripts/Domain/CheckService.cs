using System.Linq;
using Chess.Domain.Pieces;

namespace Chess.Domain
{
    public class CheckService
    {
        private readonly PieceService _pieceService;

        public CheckService(PieceService pieceService)
        {
            _pieceService = pieceService;
        }

        public bool IsCheck(Board board, PlayerColor turnPlayerColor)
        {
            var opponentKing = board.GetPiece(turnPlayerColor.Opponent(), PieceType.King);
            var myPieces = board.GetPieces(turnPlayerColor);
            var myPiecesDestinations = myPieces.SelectMany(piece => _pieceService.MoveCandidates(piece, board));
            return myPiecesDestinations.Any(pos => pos == opponentKing.Position);
        }
    }
}
