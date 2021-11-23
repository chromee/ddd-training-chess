using System;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Messages;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class PieceMoveUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly IMessagePublisher _messagePublisher;
        private readonly PieceMovementExecutor _pieceMovementExecutor;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public PieceMoveUseCase(
            GameRegistry gameRegistry,
            SelectedPieceRegistry selectedPieceRegistry,
            PieceMovementExecutor pieceMovementExecutor,
            IMessagePublisher messagePublisher)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _pieceMovementExecutor = pieceMovementExecutor;
            _messagePublisher = messagePublisher;
        }

        public void TryMovePiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece.Value;

            if (piece == null) return;

            try
            {
                _selectedPieceRegistry.Unregister();
                _pieceMovementExecutor.Move(game, piece, position.ToPosition());
            }
            catch (Exception e) when (e is WrongPlayerException or PieceNotExistOnBoardException or OutOfRangePieceMovableRangeException or SuicideMoveException)
            {
                _messagePublisher.ShowMessage(e.Message);
            }
        }
    }
}
