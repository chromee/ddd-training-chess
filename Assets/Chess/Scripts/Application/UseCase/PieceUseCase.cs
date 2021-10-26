using System;
using System.Linq;
using Chess.Application.Dto;
using Chess.Application.Services;
using Chess.Domain.Boards;
using Chess.Domain.Movements;
using UnityEngine;

namespace Chess.Application.UseCase
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
            MoveService moveService)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _moveService = moveService;
        }

        public bool SelectPiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(new Position((int)position.x, (int)position.y));

            if (piece == null || !piece.IsOwner(game.CurrentTurnPlayer)) return false;

            var pieceView = _piecesRegistry.GetPiece(position);
            _selectedPieceRegistry.Register(pieceView);
            return true;
        }

        public Vector2[] GetSelectedPieceMoveCandidates()
        {
            var piece = _selectedPieceRegistry.GetPiece(_gameRegistry.CurrentGame.Board);
            if (piece == null) throw new Exception("not found selected piece");
            return piece.MoveCandidates(_gameRegistry.CurrentGame.Board).Select(v => v.ToVector2()).ToArray();
        }

        public void TryMovePiece(Vector2 position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.GetPiece(game.Board);
            if (piece == null) return;

            _selectedPieceRegistry.Unregister();
            _moveService.Move(piece, position.ToPosition(), game);
        }
    }
}
