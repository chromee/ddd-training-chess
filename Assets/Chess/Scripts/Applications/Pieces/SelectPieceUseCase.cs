using Chess.Scripts.Applications.Games;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class SelectPieceUseCase
    {
        private readonly GameRegistry _gameRegistry;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public SelectPieceUseCase(GameRegistry gameRegistry, SelectedPieceRegistry selectedPieceRegistry)
        {
            _gameRegistry = gameRegistry;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public bool TryExecute(Vector2Int position)
        {
            var game = _gameRegistry.CurrentGame;
            var piece = game.Board.GetPiece(new Position(position.x, position.y));

            if (piece == null || piece.IsDead || !piece.IsColor(game.CurrentTurnPlayer)) return false;

            _selectedPieceRegistry.Register(piece);
            return true;
        }
    }
}
