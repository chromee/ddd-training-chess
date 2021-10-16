using Chess.Domain.Moves.Conditions;

namespace Chess.Domain.Moves
{
    public class SingleMove : MoveBase
    {
        public SingleMove(Movement direction)
        {
            Destinations = new[] { direction, };
            Conditions = new SimpleMoveConditions();
        }
    }
}
