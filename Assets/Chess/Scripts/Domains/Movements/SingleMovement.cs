using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements
{
    public class SingleMovement : Movement
    {
        public SingleMovement(MoveAmount moveAmount, IMovementCondition condition = null)
        {
            Movements = new[] { moveAmount, };
            Condition = condition ?? new BasicMovementCondition();
        }
    }
}
