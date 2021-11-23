using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Logger;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Movements.Moves;

namespace Chess.Scripts.Domains.Pieces
{
    public class PieceFactory
    {
        public Piece CreatePawn(PlayerColor color, Position position)
        {
            var dir = color == PlayerColor.White ? 1 : -1;
            var moves = new MoveBase[]
            {
                new SingleMove(new Movement(0, 1 * dir), new PawnSingleMoveCondition()),
                new SingleMove(new Movement(0, 2 * dir), new PawnDoubleMoveCondition()),
                new SingleMove(new Movement(1, 1 * dir), new PawnDiagonalMoveCondition()),
                new SingleMove(new Movement(-1, 1 * dir), new PawnDiagonalMoveCondition()),
                new SingleMove(new Movement(1, 1 * dir), new PawnEnPassantCondition()),
                new SingleMove(new Movement(-1, 1 * dir), new PawnEnPassantCondition()),
            };
            return new Piece(color, PieceType.Pawn, position, moves);
        }

        public Piece CreateKnight(PlayerColor color, Position position)
        {
            var moves = new MoveBase[]
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
            return new Piece(color, PieceType.Knight, position, moves);
        }

        public Piece CreateRook(PlayerColor color, Position position)
        {
            var moves = new MoveBase[]
            {
                new InfinityMove(new Movement(1, 0)),
                new InfinityMove(new Movement(-1, 0)),
                new InfinityMove(new Movement(0, 1)),
                new InfinityMove(new Movement(0, -1)),
            };
            return new Piece(color, PieceType.Rook, position, moves);
        }

        public Piece CreateBishop(PlayerColor color, Position position)
        {
            var moves = new MoveBase[]
            {
                new InfinityMove(new Movement(1, 1)),
                new InfinityMove(new Movement(-1, -1)),
                new InfinityMove(new Movement(1, -1)),
                new InfinityMove(new Movement(-1, 1)),
            };
            return new Piece(color, PieceType.Bishop, position, moves);
        }

        public Piece CreateQueen(PlayerColor color, Position position)
        {
            var moves = new MoveBase[]
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
            return new Piece(color, PieceType.Queen, position, moves);
        }

        public Piece CreateKing(PlayerColor color, Position position)
        {
            var moves = new MoveBase[]
            {
                new SingleMove(new Movement(1, 0)),
                new SingleMove(new Movement(-1, 0)),
                new SingleMove(new Movement(0, 1)),
                new SingleMove(new Movement(0, -1)),
                new SingleMove(new Movement(1, 1)),
                new SingleMove(new Movement(-1, -1)),
                new SingleMove(new Movement(1, -1)),
                new SingleMove(new Movement(-1, 1)),
                new SingleMove(new Movement(2, 0), new KingCastlingCondition()),
                new SingleMove(new Movement(-2, 0), new KingCastlingCondition()),
            };
            return new Piece(color, PieceType.King, position, moves);
        }
    }
}
