using System;
using System.Linq;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Messages;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class PieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly MoveService _moveService;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly IMessagePublisher _messagePublisher;

        public PieceUseCase(
            GameRegistry gameRegistry,
            SelectedPieceRegistry selectedPieceRegistry,
            MoveService moveService,
            IMessagePublisher messagePublisher)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _moveService = moveService;
            _messagePublisher = messagePublisher;
        }

        public bool SelectPiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(new Position((int)position.x, (int)position.y));

            if (piece == null || piece.IsDead || !piece.IsColor(game.CurrentTurnPlayer)) return false;

            _selectedPieceRegistry.Register(piece);
            return true;
        }

        public Vector2Int[] GetSelectedPieceMoveCandidates(Piece piece)
        {
            if (piece == null) throw new Exception("not found selected piece");
            return piece.MoveCandidates(_gameRegistry.CurrentGame).Select(v => v.ToVector2()).ToArray();
        }

        public void TryMovePiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece.Value;

            if (piece == null) return;

            try
            {
                _selectedPieceRegistry.Unregister();
                _moveService.Move(game, piece, position.ToPosition());
            }
            catch (Exception e) when (e is WrongPlayerException or PieceNotExistOnBoardException or OutOfRangePieceMovableRangeException or SuicideMoveException)
            {
                _messagePublisher.ShowMessage(e.Message);
            }
        }
    }
}
