using System;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Domain.Games
{
    public class Game
    {
        public Board Board { get; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

        private PlayerColor _currentTurnPlayerColor;

        public PlayerColor NextTurnPlayerColor =>
            _currentTurnPlayerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;

        public Player CurrentTurnPlayer => _currentTurnPlayerColor == PlayerColor.White ? _whitePlayer : _blackPlayer;


        public Game(Board board, Player whitePlayer, Player blackPlayer)
        {
            Board = board;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
        }

        public void MovePiece(Piece piece, Position position, MoveService moveService)
        {
            var turnPlayer = CurrentTurnPlayer;

            // そのターンプレイヤーでなかったとき
            if (turnPlayer.Color != _currentTurnPlayerColor)
                throw new ArgumentException($"Now is not {turnPlayer} player's turn.");

            // ピースの持ち主が違ったとき
            if (!piece.IsSameColor(turnPlayer.Color))
                throw new ArgumentException($"{piece} is not {turnPlayer} player's piece");

            moveService.Move(piece, Board, position, turnPlayer.Color);
            _currentTurnPlayerColor = NextTurnPlayerColor;
        }
    }
}
