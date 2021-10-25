using Chess.Domain.Movements.Conditions;

namespace Chess.Domain.Movements.Moves
{
    public class ConditionalMove : MoveBase
    {
        public ConditionalMove(Movement direction, IMoveConditions conditions)
        {
            Movements = new[] { direction, };
            Conditions = conditions;
        }
    }
}
