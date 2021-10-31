using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements.Moves
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
