using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Movements.Conditions;

namespace Chess.Scripts.Domains.Pieces
{
    public class PieceFactory
    {
        public Piece CreatePawn(PlayerColor color, Position position)
        {
            var dir = color == PlayerColor.White ? 1 : -1;
            var moves = new Movement[]
            {
                new SingleMovement(new MoveAmount(0, 1 * dir), new PawnSingleMovementCondition()),
                new SingleMovement(new MoveAmount(0, 2 * dir), new PawnDoubleMovementCondition()),
                new SingleMovement(new MoveAmount(1, 1 * dir), new PawnDiagonalMovementCondition()),
                new SingleMovement(new MoveAmount(-1, 1 * dir), new PawnDiagonalMovementCondition()),
                new SingleMovement(new MoveAmount(1, 1 * dir), new PawnEnPassantCondition()),
                new SingleMovement(new MoveAmount(-1, 1 * dir), new PawnEnPassantCondition()),
            };
            return new Piece(color, PieceType.Pawn, position, moves);
        }

        public Piece CreateKnight(PlayerColor color, Position position)
        {
            var moves = new Movement[]
            {
                new SingleMovement(new MoveAmount(1, 2)),
                new SingleMovement(new MoveAmount(-1, 2)),
                new SingleMovement(new MoveAmount(1, -2)),
                new SingleMovement(new MoveAmount(-1, -2)),
                new SingleMovement(new MoveAmount(2, 1)),
                new SingleMovement(new MoveAmount(2, -1)),
                new SingleMovement(new MoveAmount(-2, 1)),
                new SingleMovement(new MoveAmount(-2, -1)),
            };
            return new Piece(color, PieceType.Knight, position, moves);
        }

        public Piece CreateRook(PlayerColor color, Position position)
        {
            var moves = new Movement[]
            {
                new InfinityMovement(new MoveAmount(1, 0)),
                new InfinityMovement(new MoveAmount(-1, 0)),
                new InfinityMovement(new MoveAmount(0, 1)),
                new InfinityMovement(new MoveAmount(0, -1)),
            };
            return new Piece(color, PieceType.Rook, position, moves);
        }

        public Piece CreateBishop(PlayerColor color, Position position)
        {
            var moves = new Movement[]
            {
                new InfinityMovement(new MoveAmount(1, 1)),
                new InfinityMovement(new MoveAmount(-1, -1)),
                new InfinityMovement(new MoveAmount(1, -1)),
                new InfinityMovement(new MoveAmount(-1, 1)),
            };
            return new Piece(color, PieceType.Bishop, position, moves);
        }

        public Piece CreateQueen(PlayerColor color, Position position)
        {
            var moves = new Movement[]
            {
                new InfinityMovement(new MoveAmount(1, 0)),
                new InfinityMovement(new MoveAmount(-1, 0)),
                new InfinityMovement(new MoveAmount(0, 1)),
                new InfinityMovement(new MoveAmount(0, -1)),
                new InfinityMovement(new MoveAmount(1, 1)),
                new InfinityMovement(new MoveAmount(-1, -1)),
                new InfinityMovement(new MoveAmount(1, -1)),
                new InfinityMovement(new MoveAmount(-1, 1)),
            };
            return new Piece(color, PieceType.Queen, position, moves);
        }

        public Piece CreateKing(PlayerColor color, Position position)
        {
            var moves = new Movement[]
            {
                new SingleMovement(new MoveAmount(1, 0)),
                new SingleMovement(new MoveAmount(-1, 0)),
                new SingleMovement(new MoveAmount(0, 1)),
                new SingleMovement(new MoveAmount(0, -1)),
                new SingleMovement(new MoveAmount(1, 1)),
                new SingleMovement(new MoveAmount(-1, -1)),
                new SingleMovement(new MoveAmount(1, -1)),
                new SingleMovement(new MoveAmount(-1, 1)),
                new SingleMovement(new MoveAmount(2, 0), new KingCastlingCondition()),
                new SingleMovement(new MoveAmount(-2, 0), new KingCastlingCondition()),
            };
            return new Piece(color, PieceType.King, position, moves);
        }
    }
}
