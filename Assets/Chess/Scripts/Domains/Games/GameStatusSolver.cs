﻿using System.Linq;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Domains.Games
{
    public enum GameStatus { InProgress, Check, Checkmate, Stalemate, }

    public class GameStatusSolver
    {
        private readonly Game _game;

        public GameStatusSolver(Game game)
        {
            _game = game;
        }

        internal GameStatus SolveStatus()
        {
            if (IsCheckmate(_game.CurrentTurnPlayer)) return GameStatus.Checkmate;
            if (IsStaleMate(_game.NextTurnPlayer)) return GameStatus.Stalemate;
            if (IsCheck(_game.CurrentTurnPlayer)) return GameStatus.Check;
            return GameStatus.InProgress;
        }

        internal bool IsCheck(PlayerColor checkPlayer)
        {
            var enemyKing = _game.Board.Pieces.FirstOrDefault(v => !v.IsColor(checkPlayer) && v.IsType(PieceType.King));
            return _game.BoardStatusSolver.CanPick(enemyKing);
        }

        internal bool IsCheckmate(PlayerColor checkmatePlayer)
        {
            var targetKing = _game.Board.GetPiece(checkmatePlayer.Opponent(), PieceType.King);
            var protectors = _game.Board.Pieces.Where(v => v.IsColor(checkmatePlayer.Opponent()) && v != targetKing).ToArray();

            var killers = _game.Board.Pieces.Where(v => v.IsColor(_game.CurrentTurnPlayer)).ToArray();
            var killersMoveMap = killers.ToDictionary(v => v, piece => _game.PieceMovementCandidatesCalculator.MoveCandidates(piece));
            var checkingPiece = killersMoveMap.FirstOrDefault(v => v.Value.Any(pos => pos == targetKing.Position)).Key;

            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            if (_game.BoardStatusSolver.CanAvoid(targetKing)) return false;

            // 他のコマがチェックしてるコマを殺せるかどうか
            if (_game.BoardStatusSolver.CanKill(checkingPiece, protectors)) return false;

            // 他のコマがブロックできるorチェックしてるコマを殺せるかかどうか
            if (_game.BoardStatusSolver.CanProtect(targetKing, protectors)) return false;

            return true;
        }

        private bool IsStaleMate(PlayerColor movePlayer)
        {
            var pieces = _game.Board.GetPieces(movePlayer);

            foreach (var piece in pieces)
            {
                var destinations = _game.PieceMovementCandidatesCalculator.MoveCandidates(piece);
                foreach (var destination in destinations)
                {
                    var cloneGame = _game.Clone();
                    var clonePiece = cloneGame.Board.GetPiece(piece.Position);
                    cloneGame.Board.MovePiece(clonePiece.Position, destination);
                    if (!cloneGame.StatusSolver.IsCheck(cloneGame.CurrentTurnPlayer)) return false;
                }
            }

            return true;
        }
    }
}