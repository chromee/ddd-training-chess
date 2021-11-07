using System;
using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardUseCase
    {
        private readonly PieceUseCase _pieceUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public BoardUseCase(PieceUseCase pieceUseCase, SelectedPieceRegistry selectedPieceRegistry)
        {
            _pieceUseCase = pieceUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public void SelectBoardSquare(Vector2 position)
        {
            // コマ選択
            var select = _pieceUseCase.SelectPiece(position);
            if (select) return;

            // 移動先選択
            if (_selectedPieceRegistry.ExistSelectedPiece)
            {
                try
                {
                    _pieceUseCase.TryMovePiece(position);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
