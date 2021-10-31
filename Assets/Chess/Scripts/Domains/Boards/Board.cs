using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Boards
{
    public class Board
    {
        public List<Piece> Pieces { get; }

        private readonly List<PieceMovementLog> _log = new();
        public PieceMovementLog LastPieceMovement => _log.LastOrDefault();
        public PieceMovementLog SecondLastPieceMovement => _log.Count <= 1 ? null : _log[^2];

        public Board(List<Piece> pieces)
        {
            Pieces = pieces;

            var whiteKings = pieces.Where(v => v.IsColor(PlayerColor.White) && v.IsType(PieceType.King)).ToArray();
            if (!whiteKings.Any()) throw new NoKingException("no white king");
            if (whiteKings.Length > 1) throw new MultipleKingException("too many white kings");

            var blackKings = pieces.Where(v => v.IsColor(PlayerColor.Black) && v.IsType(PieceType.King)).ToArray();
            if (blackKings == null) throw new NoKingException("no black king");
            if (blackKings.Length > 1) throw new MultipleKingException("too many black kings");
        }

        public Piece GetPiece(Position position) => Pieces.FirstOrDefault(v => v.Position == position);
        public Piece GetPiece(Player player, PieceType type) => Pieces.FirstOrDefault(v => v.IsOwner(player) && v.IsType(type));
        public Piece[] GetPieces(Player player) => Pieces.Where(v => v.IsOwner(player)).ToArray();
        public Piece[] GetAllies(Piece piece) => Pieces.Where(v => v.IsAlly(piece)).ToArray();
        public Piece[] GetEnemies(Piece piece) => Pieces.Where(v => v.IsOpponent(piece)).ToArray();
        public bool ExistPiece(Position position) => GetPiece(position) != null;
        public bool HasPiece(Piece piece) => Pieces.Contains(piece);

        public void RemovePiece(Piece piece)
        {
            if (Pieces.Contains(piece)) Pieces.Remove(piece);
        }

        public Board Clone()
        {
            var pieces = new List<Piece>();
            foreach (var piece in Pieces)
            {
                pieces.Add(piece.Clone());
            }

            return new Board(pieces);
        }

        public void MovePiece(Position moverPosition, Position destination)
        {
            var destPiece = GetPiece(destination);
            if (destPiece != null)
            {
                destPiece.Die();
                RemovePiece(destPiece);
            }

            var movePiece = GetPiece(moverPosition);
            movePiece.Move(destination);
            _log.Add(new PieceMovementLog(movePiece, moverPosition, destination));
        }

        public bool IsCheck(Player player)
        {
            var enemyKing = Pieces.FirstOrDefault(v => !v.IsOwner(player) && v.IsType(PieceType.King));
            return CanPick(enemyKing);
        }

        /// <summary>
        /// 指定したコマを敵コマのいずれかがとれるかどうか
        /// </summary>
        /// <param name="targetPiece">標的のコマ</param>
        /// <returns></returns>
        public bool CanPick(Piece targetPiece)
        {
            var enemies = GetEnemies(targetPiece);
            var enemiesDestinations = enemies.SelectMany(piece => piece.MoveCandidates(this));
            return enemiesDestinations.Any(pos => pos == targetPiece.Position);
        }

        /// <summary>
        /// 指定したコマがとられないように回避できるかどうか
        /// </summary>
        /// <param name="avoider">標的のコマ</param>
        /// <returns></returns>
        public bool CanAvoid(Piece avoider)
        {
            var avoidDestinations = avoider.MoveCandidates(this);
            if (!CanPick(avoider)) return true;

            foreach (var destination in avoidDestinations)
            {
                var cloneBoard = Clone();
                var cloneAvoider = cloneBoard.GetPiece(avoider.Position);
                cloneBoard.MovePiece(cloneAvoider.Position, destination);
                if (!cloneBoard.CanPick(cloneAvoider)) return true;
            }

            return false;
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがとれるかどうか
        /// </summary>
        /// <param name="target">標的のコマ</param>
        /// <param name="killers">とろうとするコマたち</param>
        /// <returns></returns>
        public bool CanKill(Piece target, Piece[] killers)
        {
            return killers.Any(v => v.MoveCandidates(this).Any(pos => pos == target.Position));
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがブロックするなりして守れるかどうか
        /// </summary>
        /// <param name="protectedTarget">守られるコマ</param>
        /// <param name="protectors">守るコマたち</param>
        /// <returns></returns>
        public bool CanProtect(Piece protectedTarget, Piece[] protectors)
        {
            foreach (var protector in protectors)
            {
                var cloneBoard = Clone();
                var cloneProtector = cloneBoard.GetPiece(protector.Position);
                var cloneTarget = cloneBoard.GetPiece(protectedTarget.Position);

                foreach (var destination in cloneProtector.MoveCandidates(cloneBoard))
                {
                    cloneBoard.MovePiece(cloneProtector.Position, destination);
                    if (!cloneBoard.CanPick(cloneTarget)) return true;
                }
            }

            return false;
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
