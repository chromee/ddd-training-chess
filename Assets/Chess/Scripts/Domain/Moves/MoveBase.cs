using Chess.Domain.Moves.Conditions;
using Chess.Domain.Pieces;

namespace Chess.Domain.Moves
{
    public abstract class MoveBase
    {
        public Movement[] Destinations { get; protected set; }
        public IMoveConditions Conditions { get; protected set; }

        public bool CanMove(Piece piece, Position dest, Board board) =>
            Conditions == null || Conditions.Conditions(piece, dest, board);
    }
}
