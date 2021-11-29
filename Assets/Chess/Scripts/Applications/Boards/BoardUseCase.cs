using System;
using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardUseCase
    {
        private readonly PieceMoveUseCase _pieceMoveUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly SelectPieceUseCase _selectPieceUseCase;

        public BoardUseCase(SelectPieceUseCase selectPieceUseCase,
            PieceMoveUseCase pieceMoveUseCase,
            SelectedPieceRegistry selectedPieceRegistry
        )
        {
            _selectPieceUseCase = selectPieceUseCase;
            _pieceMoveUseCase = pieceMoveUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public void SelectBoardSquare(Vector2Int position)
        {
            // コマ選択
            var select = _selectPieceUseCase.SelectPiece(position);
            if (select) return;

            // 移動先選択
            if (_selectedPieceRegistry.ExistSelectedPiece)
            {
                try
                {
                    _pieceMoveUseCase.TryMovePiece(position);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
