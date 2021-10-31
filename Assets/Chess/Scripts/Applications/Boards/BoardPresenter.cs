using System;
using Chess.Scripts.Applications.Pieces;
using UniRx;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenter : IDisposable
    {
        private readonly PieceUseCase _pieceUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly CompositeDisposable _disposable = new();

        public BoardPresenter(PieceUseCase pieceUseCase, SelectedPieceRegistry selectedPieceRegistry)
        {
            _pieceUseCase = pieceUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public void Bind(IBoardView view)
        {
            view.OnClicked.Subscribe(position =>
            {
                view.ResetSquares();
                var selectPiece = _pieceUseCase.SelectPiece(position);

                if (selectPiece)
                {
                    // 選択した駒の移動可能範囲に色付け
                    var destinations = _pieceUseCase.GetSelectedPieceMoveCandidates();
                    foreach (var destination in destinations)
                    {
                        view.SetMovable(destination);
                    }

                    return;
                }

                if (_selectedPieceRegistry.ExistSelectedPiece)
                {
                    try
                    {
                        _pieceUseCase.TryMovePiece(position);
                        view.ResetSquares();
                    }
                    catch (ArgumentException e)
                    {
                        Debug.LogError(e);
                    }
                }
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
