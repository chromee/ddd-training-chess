﻿using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Movements.Moves;

namespace Chess.Scripts.Domains.Movements
{
    public class MoveFactory
    {
        public MoveBase[] CreatePawnMove(PlayerColor color)
        {
            var dir = color == PlayerColor.White ? 1 : -1;
            return new MoveBase[]
            {
                new ConditionalMove(new Movement(0, 1 * dir), new PawnSingleMoveCondition()),
                new ConditionalMove(new Movement(0, 2 * dir), new PawnDoubleMoveCondition()),
                new ConditionalMove(new Movement(1, 1 * dir), new PawnDiagonalMoveCondition()),
                new ConditionalMove(new Movement(-1, 1 * dir), new PawnDiagonalMoveCondition()),
                new ConditionalMove(new Movement(1, 1 * dir), new PawnEnPassantCondition()),
                new ConditionalMove(new Movement(-1, 1 * dir), new PawnEnPassantCondition()),
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