using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Domains.Boards
{
    public class Board
    {
        private readonly ReactiveCollection<Piece> _pieces;

        public Board(List<Piece> pieces)
        {
            _pieces = pieces.ToReactiveCollection();

            var whiteKings = pieces.Where(v => v.IsWhite() && v.IsType(PieceType.King)).ToArray();
            if (!whiteKings.Any()) throw new NoKingException("no white king");
            if (whiteKings.Length > 1) throw new MultipleKingException("too many white kings");

            var blackKings = pieces.Where(v => v.IsBlack() && v.IsType(PieceType.King)).ToArray();
            if (blackKings == null) throw new NoKingException("no black king");
            if (blackKings.Length > 1) throw new MultipleKingException("too many black kings");
        }

        public IReadOnlyReactiveCollection<Piece> Pieces => _pieces;

        public void MovePiece(Position moverPosition, Position destination)
        {
            var movePiece = GetPiece(moverPosition);
            if (movePiece == null) throw new ArgumentException("コマが見つかりません");

            var destPiece = GetPiece(destination);
            if (destPiece != null)
            {
                destPiece.Die();
                RemovePiece(destPiece);
            }

            movePiece.Move(destination);
        }

        public Piece GetPiece(Position position) => Pieces.FirstOrDefault(v => v.Position == position);
        public Piece[] GetPieces(PlayerColor player) => Pieces.Where(v => v.IsColor(player)).ToArray();
        public Piece GetPiece(PlayerColor player, PieceType type) => Pieces.FirstOrDefault(v => v.IsColor(player) && v.IsType(type));
        public Piece[] GetAllies(Piece piece) => Pieces.Where(v => v.IsAlly(piece)).ToArray();
        public Piece[] GetEnemies(Piece piece) => Pieces.Where(v => v.IsOpponent(piece)).ToArray();
        public bool ExistPiece(Position position) => GetPiece(position) != null;
        public bool HasPiece(Piece piece) => !piece.IsDead && Pieces.Contains(piece);

        public void AddPiece(Piece piece)
        {
            if (!_pieces.Contains(piece)) _pieces.Add(piece);
        }

        public void RemovePiece(Piece piece)
        {
            if (!_pieces.Contains(piece)) return;
            piece.Die();
            _pieces.Remove(piece);
        }

        public void RemovePiece(Position position)
        {
            var piece = GetPiece(position);
            if (piece == null) return;
            RemovePiece(piece);
        }

        public Board Clone()
        {
            var pieces = new List<Piece>();
            foreach (var piece in _pieces) pieces.Add(piece.Clone());
            return new Board(pieces);
        }
    }

    #region 例外

    public class NoKingException : Exception
    {
        public NoKingException(string message) : base(message) { }
    }

    public class MultipleKingException : Exception
    {
        public MultipleKingException(string message) : base(message) { }
    }

    #endregion
}
