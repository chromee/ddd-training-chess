using Chess.Domain.Pieces;

namespace Chess.Domain.Moves.Conditions
{
    public interface IMoveConditions
    {
        bool Conditions(Piece piece, Position destination, Board board);
    }
}
