using System;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Domain
{
    public class Game
    {
        public Board Board { get; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

        private readonly MoveService _moveService;

        private PlayerColor _currentTurnPlayerColor;

        public PlayerColor NextTurnPlayerColor =>
            _currentTurnPlayerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;

        public Player CurrentTurnPlayer => _currentTurnPlayerColor == PlayerColor.White ? _whitePlayer : _blackPlayer;


        public Game(Board board, Player whitePlayer, Player blackPlayer, MoveService moveService)
        {
            Board = board;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            _moveService = moveService;
        }

        public void MovePiece(Piece piece, Position position)
        {
            var turnPlayer = CurrentTurnPlayer;

            // そのターンプレイヤーでなかったとき
            if (turnPlayer.Color != _currentTurnPlayerColor)
                throw new ArgumentException($"Now is not {turnPlayer} player's turn.");

            // ピースの持ち主が違ったとき
            if (!piece.IsSameColor(turnPlayer.Color))
                throw new ArgumentException($"{piece} is not {turnPlayer} player's piece");

            _moveService.Move(piece, Board, position, turnPlayer.Color);
            _currentTurnPlayerColor = NextTurnPlayerColor;
        }
    }
}
