using System.Collections.Generic;
using Chess.Domain.Moves.Conditions;

namespace Chess.Domain.Moves
{
    public class InfinityMove : MoveBase
    {
        public InfinityMove(Movement direction)
        {
            Destinations = new Movement[7];
            for (var i = 0; i < 7; i++) Destinations[i] = direction * (i + 1);
            Conditions = new InfinityMoveConditions();
        }
    }
}
