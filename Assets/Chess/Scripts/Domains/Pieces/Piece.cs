using System;
using System.Collections.Generic;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Moves;
using UniRx;

namespace Chess.Scripts.Domains.Pieces
{
    public class Piece
    {
        public Player Owner { get; }
        public PieceType Type { get; }
        private readonly MoveBase[] _moves;

        public PlayerColor Color => Owner.Color;


        private readonly ReactiveProperty<Position> _position = new();
        public Position Position => _position.Value;
        public IObservable<Position> PositionAsObservable => _position;

        private readonly ReactiveProperty<bool> _isDead = new();
        public bool IsDead => _isDead.Value;
        public IObservable<bool> IsDeadAsObservable => _isDead;

        public Piece(Player owner, PieceType type, Position position, MoveBase[] moves, bool isDead = false)
        {
            Owner = owner;
            Type = type;
            _moves = moves;
            _position.Value = position;
            _isDead.Value = isDead;
        }

        public void Move(Position position) => _position.Value = position;
        public void Die() => _isDead.Value = true;

        public bool IsColor(PlayerColor color) => Color == color;
        public bool IsOwner(Player player) => Owner == player;
        public bool IsAlly(Piece piece) => Color == piece.Color;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsType(PieceType type) => Type == type;
        public override string ToString() => $"{Color} {Type}";

        public Piece Clone() => new(Owner, Type, Position, _moves, IsDead);

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
