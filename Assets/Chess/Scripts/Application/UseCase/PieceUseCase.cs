using System;
using Chess.Application.Services;
using Chess.Domain;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;

namespace Chess.Application.UseCase
{
    public class PieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly PieceService _pieceService;
        private readonly MoveService _moveService;

        public PieceUseCase(GameRegistry gameRegistry, SelectedPieceRegistry selectedPieceRegistry,
            PieceService pieceService, MoveService moveService)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
            _pieceService = pieceService;
            _moveService = moveService;
        }

        public Position[] SelectPiece(Position position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(position);

            if (piece != null && piece.IsOwner(game.CurrentTurnPlayer))
            {
                _selectedPieceRegistry.Register(piece);
                return _pieceService.MoveCandidates(piece, game.Board);
            }

            return Array.Empty<Position>();
        }

        public void TryMovePiece(Position position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = _selectedPieceRegistry.SelectedPiece;
            if (piece == null) return;

            game.MovePiece(piece, position, _moveService);
        }
    }
}
