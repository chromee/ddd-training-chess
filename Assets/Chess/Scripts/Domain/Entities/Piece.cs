using Chess.Domain.ValueObjects;

namespace Chess.Domain.Entities
{
    public abstract class Piece
    {
        private PlayerColor _color;
        public Coordinate Position { get; private set; }

        protected Piece(PlayerColor color, Coordinate position)
        {
            _color = color;
            Position = position;
        }

        public abstract Coordinate[] GetMoveCandidates(Board board);

        public virtual bool CanMove(Coordinate coordinate, Board board)
        {
            return true;
        }

        public void Move(Coordinate coordinate, Board board)
        {
            if (CanMove(coordinate, board)) Position = coordinate;
        }
    }
}
