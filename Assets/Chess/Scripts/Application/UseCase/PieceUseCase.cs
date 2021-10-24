using System;
using Chess.Application.Services;
using Chess.Domain;
using Chess.Domain.Boards;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Application.UseCase
{
    public class PieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly PieceService _pieceService;

        public PieceUseCase(
            GameRegistry gameRegistry,
            SelectedPieceRegistry selectedPieceRegistry,
            PieceService pieceService
        )
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _pieceService = pieceService;
        }

        public bool SelectPiece(Position position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(position);

            if (piece == null || !piece.IsOwner(game.CurrentTurnPlayer)) return false;

            _selectedPieceRegistry.Register(piece);
            return true;
        }

        public Position[] GetSelectedPieceMoveCandidates()
        {
            var piece = _selectedPieceRegistry.SelectedPiece;
            if (piece == null) throw new Exception("not found selected piece");
            return _pieceService.MoveCandidates(piece, _gameRegistry.CurrentGame.Board);
        }

        public void TryMovePiece(Position position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece;
            if (piece == null) return;

            _selectedPieceRegistry.Unregister();
            game.MovePiece(piece, position);
        }
    }
}
