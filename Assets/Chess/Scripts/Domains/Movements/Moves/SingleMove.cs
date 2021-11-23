using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public class SingleMove : MoveBase
    {
        public SingleMove(Movement movement, IMoveCondition condition = null)
        {
            Movements = new[] { movement, };
            Condition = condition ?? new BasicMoveCondition();
        }
    }
}
