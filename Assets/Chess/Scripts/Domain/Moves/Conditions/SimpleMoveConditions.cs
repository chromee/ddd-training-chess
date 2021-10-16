using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public class SimpleMoveConditions : IMoveConditions
    {
        public bool Conditions(Piece piece, Position destination, Board board)
        {
            var destPiece = board.GetPiece(destination);
            return !piece.IsCorrectOwner(piece.Color);
        }
    }
}
