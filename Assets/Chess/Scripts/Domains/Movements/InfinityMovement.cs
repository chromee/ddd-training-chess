using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Movements
{
    public class InfinityMovement : Movement
    {
        public InfinityMovement(MoveAmount direction)
        {
            Movements = new MoveAmount[7];
            for (var i = 0; i < 7; i++)
            {
                Movements[i] = direction * (i + 1);
            }

            Condition = new InfinityMovementCondition();
        }
    }
}
