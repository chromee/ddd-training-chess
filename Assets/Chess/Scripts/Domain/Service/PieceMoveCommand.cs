using Chess.Domain.Entities;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Service
{
    public class PieceMoveCommand
    {
        public PieceMoveCommand(PlayerColor color, Piece piece)
        {
            Color = color;
            Piece = piece;
        }

        public PlayerColor Color { get; private set; }
        public Piece Piece { get; private set; }
    }
}
