using Chess.Domain.Moves.Conditions;

namespace Chess.Domain.Moves
{
    public class ConditionalMove : MoveBase
    {
        public ConditionalMove(Movement direction, IMoveConditions conditions)
        {
            Destinations = new[] { direction, };
            Conditions = conditions;
        }
    }
}
