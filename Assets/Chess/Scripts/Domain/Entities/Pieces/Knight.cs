using Chess.Domain.ValueObjects;

namespace Chess.Domain.Entities.Pieces
{
    public class Knight : Piece
    {
        public Knight(PlayerColor color, Coordinate position) : base(color, position)
        {
        }

        public override Coordinate[] GetMoveCandidates(Board board) => throw new System.NotImplementedException();
    }
}
