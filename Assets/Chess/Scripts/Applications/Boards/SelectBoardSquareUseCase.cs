using System;
using Chess.Scripts.Applications.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class SelectBoardSquareUseCase
    {
        private readonly MovePieceUseCase _movePieceUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly SelectPieceUseCase _selectPieceUseCase;

        public SelectBoardSquareUseCase(SelectPieceUseCase selectPieceUseCase,
            MovePieceUseCase movePieceUseCase,
            SelectedPieceRegistry selectedPieceRegistry
        )
        {
            _selectPieceUseCase = selectPieceUseCase;
            _movePieceUseCase = movePieceUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public void Execute(Vector2Int position)
        {
            // コマ選択
            var select = _selectPieceUseCase.TryExecute(position);
            if (select) return;

            // 移動先選択
            if (_selectedPieceRegistry.ExistSelectedPiece)
            {
                try
                {
                    _movePieceUseCase.Execute(position);
                }
                catch (ArgumentException e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
