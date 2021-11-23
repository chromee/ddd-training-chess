﻿using Chess.Scripts.Applications.Pieces;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenterFactory
    {
        private readonly PieceUseCase _pieceUseCase;
        private readonly BoardUseCase _boardUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public BoardPresenterFactory(PieceUseCase pieceUseCase, BoardUseCase boardUseCase, SelectedPieceRegistry selectedPieceRegistry)
        {
            _pieceUseCase = pieceUseCase;
            _boardUseCase = boardUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public BoardPresenter Create(IBoardView view) => new BoardPresenter(_pieceUseCase, _boardUseCase, _selectedPieceRegistry, view);
    }
}