using System;
using System.Linq;
using Chess.Domain.Pieces;

namespace Chess.Domain
{
    public class Game
    {
        public Board Board { get; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

        private PlayerColor _currentTurnPlayerColor;

        private PlayerColor NextTurnPlayerColor =>
            _currentTurnPlayerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;

        public Game(Board board, Player whitePlayer, Player blackPlayer)
        {
            Board = board;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            _currentTurnPlayerColor = PlayerColor.White;
        }

        public void MovePiece(PlayerColor playerColor, Piece piece, Position position)
        {
            // ピースがボードになかったとき
            if (!Board.HasPiece(piece))
                throw new ArgumentException("the piece is not on board.");

            // そのターンプレイヤーでなかったとき
            if (playerColor != _currentTurnPlayerColor)
                throw new ArgumentException($"Now is not {playerColor} player's turn.");

            // ピースの持ち主が違ったとき
            if (!piece.IsCorrectOwner(playerColor))
                throw new ArgumentException($"{piece} is not {playerColor} player's piece");

            piece.Move(position, Board);
            _currentTurnPlayerColor = NextTurnPlayerColor;
        }

        public bool IsCheck(PlayerColor color)
        {
            var king = Board.GetPiece(color, PieceType.King);
            return Board.Pieces.Where(v => v.IsCorrectOwner(_currentTurnPlayerColor))
                .Any(v => v.MoveCandidates(Board).Any(v => v == king.Position));
        }

        public bool IsCheckMate(PlayerColor color)
        {
            var king = Board.GetPiece(color, PieceType.King);
            var alliesPieces = Board.Pieces.Where(v => v.IsCorrectOwner(color));
            var enemyPieces = Board.Pieces.Where(v => !v.IsCorrectOwner(color));
            var alliesPiecesMoves = alliesPieces.ToDictionary(v => v, v => v.MoveCandidates(Board));
            var enemyPiecesMoves = enemyPieces.ToDictionary(v => v, v => v.MoveCandidates(Board));

            var checkingPiece = enemyPiecesMoves.FirstOrDefault(v => v.Value.Any(pos => pos == king.Position)).Key;
            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            var kindDestinations = alliesPiecesMoves[king];
            var avoidanceFlags = new bool[kindDestinations.Length];
            for (var i = 0; i < avoidanceFlags.Length; i++) avoidanceFlags[i] = true;
            foreach (var destinations in enemyPiecesMoves.Values)
            {
                foreach (var destination in destinations)
                {
                    var i = Array.IndexOf(kindDestinations, destination);
                    if (i > 0) avoidanceFlags[i] = false;
                }
            }

            var canAvoid = avoidanceFlags.Any(v => v);
            if (canAvoid) return false;

            // 他のコマがチェックしてるコマを殺せるかどうか
            var canKill = alliesPiecesMoves.Values.Any(v => v.Any(pos => pos == checkingPiece.Position));
            if (canKill) return false;

            // TODO: 他のコマがブロックできるかどうか
            var canBlock = false;
            if (canBlock) return false;

            return true;
        }
    }
}
