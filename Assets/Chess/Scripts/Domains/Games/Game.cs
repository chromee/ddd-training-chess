using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Logger;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;

namespace Chess.Scripts.Domains.Games
{
    public enum GameStatus { InProgress, Check, Checkmate, Stalemate, }

    public class Game
    {
        private readonly GameService _gameService = new();

        public Board Board { get; }
        public PieceMovementLogger Logger { get; }

        public PlayerColor CurrentTurnPlayer { get; private set; }
        public PlayerColor NextTurnPlayer => CurrentTurnPlayer.Opponent();

        private readonly ReactiveProperty<GameStatus> _gameStatus = new();
        public IReadOnlyReactiveProperty<GameStatus> GameStatus => _gameStatus;

        public Game(Board board, PieceMovementLogger logger = null)
        {
            Board = board;
            Logger = logger ?? new PieceMovementLogger();

            // 先行は白プレイヤー
            CurrentTurnPlayer = PlayerColor.White;
        }

        public void SwapTurn()
        {
            if (_gameService.IsCheckmate(this, CurrentTurnPlayer))
            {
                _gameStatus.Value = Games.GameStatus.Checkmate;
            }
            else if (_gameService.IsStaleMate(this, NextTurnPlayer))
            {
                _gameStatus.Value = Games.GameStatus.Stalemate;
            }
            else if (_gameService.IsCheck(this, CurrentTurnPlayer))
            {
                _gameStatus.Value = Games.GameStatus.Check;
            }
            else
            {
                _gameStatus.Value = Games.GameStatus.InProgress;
            }

            CurrentTurnPlayer = NextTurnPlayer;
        }

        public Game Clone()
        {
            var cloneBoard = Board.Clone();
            var logger = Logger.Clone();
            return new Game(cloneBoard, logger);
        }
    }
}
