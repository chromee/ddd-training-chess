using System;
using Chess.Scripts.Applications.Pieces;
using UniRx;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenter : IDisposable
    {
        private readonly PieceUseCase _pieceUseCase;
        private readonly BoardUseCase _boardUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        private readonly CompositeDisposable _disposable = new();

        public BoardPresenter(PieceUseCase pieceUseCase, BoardUseCase boardUseCase, SelectedPieceRegistry selectedPieceRegistry)
        {
            _pieceUseCase = pieceUseCase;
            _boardUseCase = boardUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public void Bind(IBoardView view)
        {
            // 選択中のコマが変わったら移動可能マスの表示を更新
            _selectedPieceRegistry.SelectedPiece.Subscribe(piece =>
            {
                view.ResetSquares();

                if (piece == null) return;

                var destinations = _pieceUseCase.GetSelectedPieceMoveCandidates(piece);
                foreach (var destination in destinations) view.SetMovable(destination);
            }).AddTo(_disposable);

            // コマ選択 or 移動先選択
            view.OnClicked.Subscribe(position =>
            {
                _boardUseCase.SelectBoardSquare(position);
                if (_selectedPieceRegistry.SelectedPiece.Value == null) view.ResetSquares();
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
