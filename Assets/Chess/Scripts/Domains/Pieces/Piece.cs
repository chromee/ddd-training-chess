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
        public PlayerColor Color { get; }
        public PieceType Type { get; }
        private readonly MoveBase[] _moves;

        private readonly ReactiveProperty<Position> _position = new();
        public Position Position => _position.Value;
        public IObservable<Position> PositionAsObservable => _position;

        private readonly ReactiveProperty<bool> _isDead = new();
        public bool IsDead => _isDead.Value;
        public IObservable<bool> IsDeadAsObservable => _isDead;

        public Piece(PlayerColor color, PieceType type, Position position, MoveBase[] moves, bool isDead = false)
        {
            Color = color;
            Type = type;
            _moves = moves;
            _position.Value = position;
            _isDead.Value = isDead;
        }

        public void Move(Position position) => _position.Value = position;
        public void Die() => _isDead.Value = true;

        public bool IsColor(PlayerColor player) => Color == player;
        public bool IsWhite() => IsColor(PlayerColor.White);
        public bool IsBlack() => IsColor(PlayerColor.Black);
        public bool IsAlly(Piece piece) => Color == piece.Color;
        public bool IsOpponent(Piece piece) => Color != piece.Color;
        public bool IsType(PieceType type) => Type == type;
        public override string ToString() => $"{Color} {Type}";

        public Piece Clone() => new(Color, Type, Position, _moves, IsDead);

        public Position[] MoveCandidates(Game game)
        {
            var candidates = new List<Position>();

            foreach (var move in _moves)
            {
                foreach (var movement in move.Movements)
                {
                    try
                    {
                        var destination = Position + movement;
                        if (!move.CanExecute(game, this, destination)) continue;
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
