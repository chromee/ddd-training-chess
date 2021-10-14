using Chess.Domain.Entities;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Service
{
    public class GameFlowService
    {
        public Board InitializeBoard()
        {
            var board = new Board();

            PieceService.GeneratePiece(board, PlayerColor.Black);
            PieceService.GeneratePiece(board, PlayerColor.White);

            return board;
        }
    }
}
