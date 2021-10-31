using System;
using System.Linq;
using Chess.Scripts.Applications.Game;
using Chess.Scripts.Applications.Messages;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class PieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly PiecesRegistry _piecesRegistry;
        private readonly MoveService _moveService;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly IMessagePublisher _messagePublisher;

        public PieceUseCase(
            GameRegistry gameRegistry,
            SelectedPieceRegistry selectedPieceRegistry,
            MoveService moveService,
            PiecesRegistry piecesRegistry, IMessagePublisher messagePublisher)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _moveService = moveService;
            _piecesRegistry = piecesRegistry;
            _messagePublisher = messagePublisher;
        }

        public bool SelectPiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(new Position((int)position.x, (int)position.y));

            if (piece == null || !piece.IsOwner(game.CurrentTurnPlayer)) return false;

            _selectedPieceRegistry.Register(piece);
            return true;
        }

        public Vector2[] GetSelectedPieceMoveCandidates(Piece piece)
        {
            if (piece == null) throw new Exception("not found selected piece");
            return piece.MoveCandidates(_gameRegistry.CurrentGame.Board).Select(v => v.ToVector2()).ToArray();
        }

        public void TryMovePiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece.Value;

            if (piece == null) return;

            _selectedPieceRegistry.Unregister();
            try
            {
                _moveService.Move(piece, position.ToPosition(), game);
            }
            catch (Exception e)
            {
                _messagePublisher.ShowMessage(e.Message);
            }
        }

        public void UpdatePieces()
        {
            foreach (var piecePresenter in _piecesRegistry.Pieces)
            {
                piecePresenter.UpdateView();
            }
        }
    }
}
