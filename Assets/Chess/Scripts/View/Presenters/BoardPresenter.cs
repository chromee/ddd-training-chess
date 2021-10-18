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
                var selectedPiece = _selectedPieceRegistry.SelectedPiece;
                if (selectedPiece != null)
                {
                    try
                    {
                        _pieceUseCase.TryMovePiece(position);
                        view.ResetSquares();
                        _selectedPieceRegistry.Unregister();
                    }
                    catch (ArgumentException e)
                    {
                        Debug.LogError(e);
                    }
                }
                else
                {
                    view.ResetSquares();
                    var destinations = _pieceUseCase.SelectPiece(position);
                    foreach (var destination in destinations)
                    {
                        view.SetMovable(destination);
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
