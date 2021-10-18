using Chess.Domain.Movements.Moves;

namespace Chess.Domain.Pieces
{
    public class Piece
    {
        private readonly Player _owner;
        public MoveBase[] Moves { get; }
        public PieceType Type { get; }
        public Position Position { get; private set; }
        public bool Dead { get; private set; }

        public Piece(Player owner, PieceType type, Position position, MoveBase[] moves)
        {
            _owner = owner;
            Type = type;
            Position = position;
            Moves = moves;
            Dead = false;
        }

        public void Move(Position position) => Position = position;
        public void Die() => Dead = true;

        public PlayerColor Color => _owner.Color;
        public bool IsSameColor(PlayerColor color) => Color == color;
        public bool IsOwner(Player player) => _owner == player;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsSameType(PieceType type) => Type == type;
        public bool IsSamePosition(Position position) => Position == position;
        public override string ToString() => $"{Color} {Type}";
        public Piece Clone() => (Piece)MemberwiseClone();
    }
}
