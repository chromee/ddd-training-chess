using Chess.Domain.Movements.Conditions;
using Chess.Domain.Pieces;

namespace Chess.Domain.Movements.Moves
{
    public abstract class MoveBase
    {
        public Movement[] Destinations { get; protected set; }
        public IMoveConditions Conditions { get; protected set; }

        public bool CanMove(Piece piece, Position dest, Board board) =>
            Conditions == null || Conditions.CanExecute(piece, dest, board);
    }
}
