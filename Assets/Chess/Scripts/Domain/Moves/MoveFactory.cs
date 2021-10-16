using Chess.Domain.Moves.Conditions;

namespace Chess.Domain.Moves
{
    public class MoveFactory
    {
        public MoveBase[] CreatePawnMove()
        {
            return new MoveBase[]
            {
                new ConditionalMove(new Movement(0, 1), new PawnSingleMoveCondition()),
                new ConditionalMove(new Movement(0, 2), new PawnDoubleMoveCondition()),
                new ConditionalMove(new Movement(1, 1), new PawnDiagonalMoveCondition()),
                new ConditionalMove(new Movement(-1, 1), new PawnDiagonalMoveCondition()),
            };
        }

        public MoveBase[] CreateKnightMove()
        {
            return new MoveBase[]
            {
                new SingleMove(new Movement(1, 2)),
                new SingleMove(new Movement(-1, 2)),
                new SingleMove(new Movement(1, -2)),
                new SingleMove(new Movement(-1, -2)),
                new SingleMove(new Movement(2, 1)),
                new SingleMove(new Movement(2, -1)),
                new SingleMove(new Movement(-2, 1)),
                new SingleMove(new Movement(-2, -1)),
            };
        }

        public MoveBase[] CreateRookMove()
        {
            return new MoveBase[]
            {
                new InfinityMove(new Movement(1, 0)),
                new InfinityMove(new Movement(-1, 0)),
                new InfinityMove(new Movement(0, 1)),
                new InfinityMove(new Movement(0, -1)),
            };
        }

        public MoveBase[] CreateBishopMove()
        {
            return new MoveBase[]
            {
                new InfinityMove(new Movement(1, 1)),
                new InfinityMove(new Movement(-1, -1)),
                new InfinityMove(new Movement(1, -1)),
                new InfinityMove(new Movement(-1, 1)),
            };
        }

        public MoveBase[] CreateQueenMove()
        {
            return new MoveBase[]
            {
                new InfinityMove(new Movement(1, 0)),
                new InfinityMove(new Movement(-1, 0)),
                new InfinityMove(new Movement(0, 1)),
                new InfinityMove(new Movement(0, -1)),
                new InfinityMove(new Movement(1, 1)),
                new InfinityMove(new Movement(-1, -1)),
                new InfinityMove(new Movement(1, -1)),
                new InfinityMove(new Movement(-1, 1)),
            };
        }

        public MoveBase[] CreateKingMove()
        {
            return new MoveBase[]
            {
                new SingleMove(new Movement(1, 0)),
                new SingleMove(new Movement(-1, 0)),
                new SingleMove(new Movement(0, 1)),
                new SingleMove(new Movement(0, -1)),
                new SingleMove(new Movement(1, 1)),
                new SingleMove(new Movement(-1, -1)),
                new SingleMove(new Movement(1, -1)),
                new SingleMove(new Movement(-1, 1)),
            };
        }
    }
}
