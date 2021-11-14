using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public class SingleMove : MoveBase
    {
        public SingleMove(Movement movement, IMoveConditions conditions = null)
        {
            Movements = new[] { movement, };
            Conditions = conditions ?? new BasicMoveConditions();
        }
    }
}
