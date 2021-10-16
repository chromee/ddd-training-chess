using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Moves;
using Unity.VisualScripting;

namespace Chess.Domain.Pieces
{
    public class Piece
    {
        private readonly Player _owner;
        private readonly MoveBase[] _moves;
        public PieceType Type { get; }
        public Position Position { get; private set; }

        public Piece(Player owner, PieceType type, Position position, MoveBase[] moves)
        {
            _owner = owner;
            Type = type;
            Position = position;
            _moves = moves;
        }

        public PlayerColor Color => _owner.Color;

        public void Move(Position position, Board board)
        {
            if (!CanMove(position, board)) throw new ArgumentException("cannot move this destination");
            Position = position;
        }

        public bool CanMove(Position position, Board board)
        {
            var candidates = MoveCandidates(board);
            return candidates.Contains(position);
        }

        //TODO: 切り出す
        public Position[] MoveCandidates(Board board)
        {
            var candidates = new List<Position>();

            foreach (var move in _moves)
            {
                foreach (var relativeDest in move.Destinations)
                {
                    try
                    {
                        var destination = Position + relativeDest;
                        if (move.CanMove(this, destination, board)) candidates.Add(destination);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        break;
                    }
                }
            }

            return candidates.ToArray();
        }

        public bool IsCorrectOwner(PlayerColor color) => Color == color;
        public bool IsSameType(PieceType type) => Type == type;

        public override string ToString() => $"{Color} {Type}";
    }
}
