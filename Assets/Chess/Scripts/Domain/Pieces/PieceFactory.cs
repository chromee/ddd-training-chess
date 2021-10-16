using Chess.Domain.Moves;
using Chess.Domain.Moves.Conditions;

namespace Chess.Domain.Pieces
{
    public class PieceFactory
    {
        private readonly MoveFactory _moveFactory;

        public PieceFactory()
        {
            _moveFactory = new MoveFactory();
        }

        public Piece[] CreatePieces(Player player)
        {
            var pawnLine = player.Color == PlayerColor.White ? 1 : 6;
            var othersLine = player.Color == PlayerColor.White ? 0 : 7;
            var pieces = new Piece[16];
            for (var i = 0; i < 8; i++) pieces[i] = CreatePawn(player, new Position(i, pawnLine));
            pieces[8] = CreateRook(player, new Position(0, othersLine));
            pieces[9] = CreateKnight(player, new Position(1, othersLine));
            pieces[10] = CreateBishop(player, new Position(2, othersLine));
            pieces[11] = CreateQueen(player, new Position(3, othersLine));
            pieces[12] = CreateKing(player, new Position(4, othersLine));
            pieces[13] = CreateBishop(player, new Position(5, othersLine));
            pieces[14] = CreateKnight(player, new Position(6, othersLine));
            pieces[15] = CreateRook(player, new Position(7, othersLine));
            return pieces;
        }

        private Piece CreatePawn(Player player, Position position)
        {
            return new Piece(player, PieceType.Pawn, position, _moveFactory.CreatePawnMove());
        }

        private Piece CreateKnight(Player player, Position position)
        {
            return new Piece(player, PieceType.Knight, position, _moveFactory.CreateKnightMove());
        }

        private Piece CreateRook(Player player, Position position)
        {
            return new Piece(player, PieceType.Rook, position, _moveFactory.CreateRookMove());
        }

        private Piece CreateBishop(Player player, Position position)
        {
            return new Piece(player, PieceType.Bishop, position, _moveFactory.CreateBishopMove());
        }

        private Piece CreateQueen(Player player, Position position)
        {
            return new Piece(player, PieceType.Queen, position, _moveFactory.CreateQueenMove());
        }

        private Piece CreateKing(Player player, Position position)
        {
            return new Piece(player, PieceType.King, position, _moveFactory.CreateKingMove());
        }
    }
}
