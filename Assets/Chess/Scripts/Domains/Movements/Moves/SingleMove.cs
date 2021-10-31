using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public class SingleMove : MoveBase
    {
        public SingleMove(Movement direction)
        {
            Movements = new[] { direction, };
            Conditions = new BasicMoveConditions();
        }
    }
}
