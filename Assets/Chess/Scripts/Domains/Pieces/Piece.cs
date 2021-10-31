using System;
using System.Collections.Generic;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.HandLogs;
using Chess.Scripts.Domains.Movements.Moves;

namespace Chess.Scripts.Domains.Pieces
{
    public class Piece
    {
        private readonly Player _owner;
        private readonly MoveBase[] _moves;

        public PlayerColor Color => _owner.Color;
        public PieceType Type { get; }
        public Position Position { get; private set; }
        public bool IsDead { get; private set; }

        public Piece(Player owner, PieceType type, Position position, MoveBase[] moves)
        {
            _owner = owner;
            Type = type;
            Position = position;
            _moves = moves;
            IsDead = false;
        }

        public void Move(Position position) => Position = position;
        public void Die() => IsDead = true;

        public bool IsSameColor(PlayerColor color) => Color == color;
        public bool IsOwner(Player player) => _owner == player;
        public bool IsAlly(Piece piece) => Color == piece.Color;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsSameType(PieceType type) => Type == type;
        public override string ToString() => $"{Color} {Type}";

        public Piece Clone() => (Piece)MemberwiseClone();

        public Position[] MoveCandidates(Board board)
        {
            var candidates = new List<Position>();

            foreach (var move in _moves)
            {
                foreach (var movement in move.Movements)
                {
                    try
                    {
                        var destination = Position + movement;
                        if (!move.CanExecute(this, destination, board)) continue;
                        candidates.Add(destination);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Destinationsの中身が複数あるのはInfinityMoveだけなので、
                        // 範囲外になったら(Positionのコンストラクタでthrowされたら)break
                        break;
                    }
                }
            }

            return candidates.ToArray();
        }
    }
}
