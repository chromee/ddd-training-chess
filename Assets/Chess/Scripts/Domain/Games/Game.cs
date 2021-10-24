using System;
using Chess.Domain.Boards;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Domain.Games
{
    public class Game
    {
        public Board Board { get; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;
        private readonly MoveService _moveService;

        public Player CurrentTurnPlayer { get; private set; }
        public Player NextTurnPlayer => CurrentTurnPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;


        public Game(Board board, Player whitePlayer, Player blackPlayer, MoveService moveService)
        {
            Board = board;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;

            _moveService = moveService;

            // 先行は白プレイヤー
            CurrentTurnPlayer = whitePlayer;
        }

        public void MovePiece(Piece piece, Position position)
        {
            // そのターンプレイヤーでなかったとき
            if (!piece.IsOwner(CurrentTurnPlayer))
                throw new WrongPlayerException($"This turn is not {CurrentTurnPlayer}'s turn.");

            _moveService.Move(piece, Board, position, CurrentTurnPlayer, NextTurnPlayer);
            CurrentTurnPlayer = NextTurnPlayer;
        }
    }

    public class WrongPlayerException : Exception
    {
        public WrongPlayerException(string message) : base(message)
        {
        }
    }
}
