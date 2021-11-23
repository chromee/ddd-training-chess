﻿using System.Linq;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Domains.Games
{
    public enum GameStatus { InProgress, Check, Checkmate, Stalemate, }

    public class GameStatusHandler
    {
        private readonly Game _game;

        private readonly ReactiveProperty<GameStatus> _gameStatus = new();

        public GameStatus CurrentStatus => _gameStatus.Value;
        public IReadOnlyReactiveProperty<GameStatus> CurrentStatusObservable => _gameStatus;

        public GameStatusHandler(Game game)
        {
            _game = game;
        }

        public void UpdateStatus()
        {
            if (IsCheckmate(_game.CurrentTurnPlayer))
            {
                _gameStatus.Value = GameStatus.Checkmate;
            }
            else if (IsStaleMate(_game.NextTurnPlayer))
            {
                _gameStatus.Value = GameStatus.Stalemate;
            }
            else if (IsCheck(_game.CurrentTurnPlayer))
            {
                _gameStatus.Value = GameStatus.Check;
            }
            else
            {
                _gameStatus.Value = GameStatus.InProgress;
            }
        }

        public bool IsCheck(PlayerColor checkPlayer)
        {
            var enemyKing = _game.Board.Pieces.FirstOrDefault(v => !v.IsColor(checkPlayer) && v.IsType(PieceType.King));
            return CanPick(enemyKing);
        }

        public bool IsCheckmate(PlayerColor checkmatePlayer)
        {
            var targetKing = _game.Board.GetPiece(checkmatePlayer.Opponent(), PieceType.King);
            var protectors = _game.Board.Pieces.Where(v => v.IsColor(checkmatePlayer.Opponent()) && v != targetKing).ToArray();

            var killers = _game.Board.Pieces.Where(v => v.IsColor(_game.CurrentTurnPlayer)).ToArray();
            var killersMoveMap = killers.ToDictionary(v => v, piece => piece.MoveCandidates(_game));
            var checkingPiece = killersMoveMap.FirstOrDefault(v => v.Value.Any(pos => pos == targetKing.Position)).Key;

            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            if (CanAvoid(targetKing)) return false;

            // 他のコマがチェックしてるコマを殺せるかどうか
            if (CanKill(checkingPiece, protectors)) return false;

            // 他のコマがブロックできるorチェックしてるコマを殺せるかかどうか
            if (CanProtect(targetKing, protectors)) return false;

            return true;
        }

        public bool IsStaleMate(PlayerColor movePlayer)
        {
            var pieces = _game.Board.GetPieces(movePlayer);

            foreach (var piece in pieces)
            {
                var destinations = piece.MoveCandidates(_game);
                foreach (var destination in destinations)
                {
                    var cloneGame = _game.Clone();
                    var clonePiece = cloneGame.Board.GetPiece(piece.Position);
                    cloneGame.Board.MovePiece(clonePiece.Position, destination);
                    if (!cloneGame.StatusHandler.IsCheck(cloneGame.CurrentTurnPlayer)) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 指定したコマを敵コマのいずれかがとれるかどうか
        /// </summary>
        public bool CanPick(Piece targetPiece)
        {
            var enemies = _game.Board.GetEnemies(targetPiece);
            var enemiesDestinations = enemies.SelectMany(piece => piece.MoveCandidates(_game));
            return enemiesDestinations.Any(pos => pos == targetPiece.Position);
        }

        /// <summary>
        /// 指定したコマがとられないように回避できるかどうか
        /// </summary>
        public bool CanAvoid(Piece avoider)
        {
            var avoidDestinations = avoider.MoveCandidates(_game);
            if (!CanPick(avoider)) return true;

            foreach (var destination in avoidDestinations)
            {
                var cloneGame = _game.Clone();
                var cloneAvoider = cloneGame.Board.GetPiece(avoider.Position);
                cloneGame.Board.MovePiece(cloneAvoider.Position, destination);
                if (!cloneGame.StatusHandler.CanPick(cloneAvoider)) return true;
            }

            return false;
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがとれるかどうか
        /// </summary>
        public bool CanKill(Piece target, Piece[] killers)
        {
            return killers.Any(v => v.MoveCandidates(_game).Any(pos => pos == target.Position));
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがブロックするなりして守れるかどうか
        /// </summary>
        public bool CanProtect(Piece protectedTarget, Piece[] protectors)
        {
            foreach (var protector in protectors)
            {
                var cloneGame = _game.Clone();
                var cloneProtector = cloneGame.Board.GetPiece(protector.Position);
                var cloneTarget = cloneGame.Board.GetPiece(protectedTarget.Position);

                foreach (var destination in cloneProtector.MoveCandidates(cloneGame))
                {
                    cloneGame.Board.MovePiece(cloneProtector.Position, destination);
                    if (!cloneGame.StatusHandler.CanPick(cloneTarget)) return true;
                }
            }

            return false;
        }
    }
}