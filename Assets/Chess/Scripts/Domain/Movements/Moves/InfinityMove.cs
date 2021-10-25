﻿using Chess.Domain.Movements.Conditions;

namespace Chess.Domain.Movements.Moves
{
    public class InfinityMove : MoveBase
    {
        public InfinityMove(Movement direction)
        {
            Movements = new Movement[7];
            for (var i = 0; i < 7; i++) Movements[i] = direction * (i + 1);
            Conditions = new InfinityMoveConditions();
        }
    }
}
