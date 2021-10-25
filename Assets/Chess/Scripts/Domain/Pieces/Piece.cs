using System;
using System.Collections.Generic;
using Chess.Domain.Boards;
using Chess.Domain.Games;
using Chess.Domain.Movements.Moves;

namespace Chess.Domain.Pieces
{
    public class Piece
    {
        public Player Owner { get; }
        public PlayerColor Color => Owner.Color;
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

        public bool IsSameColor(PlayerColor color) => Color == color;
        public bool IsOwner(Player player) => Owner == player;
        public bool IsAlly(Piece piece) => Color == piece.Color;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsSameType(PieceType type) => Type == type;
        public override string ToString() => $"{Color} {Type}";

        public Piece Clone() => (Piece)MemberwiseClone();

        public Position[] MoveCandidates(Board board)
        {
            var candidates = new List<Position>();

            foreach (var move in Moves)
            {
                foreach (var movement in move.Movements)
                {
                    try
                    {
                        var destination = Position + movement.ConsiderColor(Owner);
                        if (!move.Conditions.CanExecute(this, destination, board)) continue;
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
