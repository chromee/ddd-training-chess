using Chess.Domain.Boards;
using Chess.Domain.Movements.Moves;

namespace Chess.Domain.Pieces
{
    public class Piece
    {
        public Player Owner { get; }
        public MoveBase[] Moves { get; }
        public PieceType Type { get; }
        public Position Position { get; private set; }
        public bool Dead { get; private set; }

        public Piece(Player owner, PieceType type, Position position, MoveBase[] moves)
        {
            Owner = owner;
            Type = type;
            Position = position;
            Moves = moves;
            Dead = false;
        }

        public void Move(Position position) => Position = position;
        public void Die() => Dead = true;

        public PlayerColor Color => Owner.Color;
        public bool IsSameColor(PlayerColor color) => Color == color;
        public bool IsOwner(Player player) => Owner == player;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsSameType(PieceType type) => Type == type;
        public bool IsSamePosition(Position position) => Position == position;
        public override string ToString() => $"{Color} {Type}";
        public Piece Clone() => (Piece)MemberwiseClone();
    }
}
