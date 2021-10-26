using System;
using Chess.Application.interfaces;
using Chess.Application.Services;
using Chess.Application.UseCase;
using UniRx;
using UnityEngine;

namespace Chess.View.Presenters
{
    public class BoardPresenter : IBoardPresenter, IDisposable
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
                var success = _pieceUseCase.SelectPiece(position);

                if (success)
                {
                    // 選択した駒の移動可能範囲に色付け
                    var destinations = _pieceUseCase.GetSelectedPieceMoveCandidates();
                    foreach (var destination in destinations)
                    {
                        view.SetMovable(destination);
                    }

                    return;
                }

                if (_selectedPieceRegistry.SelectedPiece != null)
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
