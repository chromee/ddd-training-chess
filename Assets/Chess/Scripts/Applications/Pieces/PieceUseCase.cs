using System;
using System.Linq;
using Chess.Scripts.Applications.Game;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Movements;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class PieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly PiecesRegistry _piecesRegistry;
        private readonly MoveService _moveService;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public PieceUseCase(
            GameRegistry gameRegistry,
            SelectedPieceRegistry selectedPieceRegistry,
            MoveService moveService,
            PiecesRegistry piecesRegistry
        )
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _moveService = moveService;
            _piecesRegistry = piecesRegistry;
        }

        public bool SelectPiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(new Position((int)position.x, (int)position.y));

            if (piece == null || !piece.IsOwner(game.CurrentTurnPlayer)) return false;

            _selectedPieceRegistry.Register(piece);
            return true;
        }

        public Vector2[] GetSelectedPieceMoveCandidates()
        {
            var piece = _selectedPieceRegistry.SelectedPiece;
            if (piece == null) throw new Exception("not found selected piece");
            return piece.MoveCandidates(_gameRegistry.CurrentGame.Board).Select(v => v.ToVector2()).ToArray();
        }

        public void TryMovePiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece;

            if (piece == null) return;

            _selectedPieceRegistry.Unregister();
            _moveService.Move(piece, position.ToPosition(), game);

            UpdatePieces();
        }

        private void UpdatePieces()
        {
            foreach (var piecePresenter in _piecesRegistry.Pieces)
            {
                piecePresenter.UpdateView();
            }
        }
    }
}
