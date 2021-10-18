using Chess.Domain.Movements.Conditions;

namespace Chess.Domain.Movements.Moves
{
    public class SingleMove : MoveBase
    {
        public SingleMove(Movement direction)
        {
            Destinations = new[] { direction, };
            Conditions = new BasicMoveConditions();
        }
    }
}
