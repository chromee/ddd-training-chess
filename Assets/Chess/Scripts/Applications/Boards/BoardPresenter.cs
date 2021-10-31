using System;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Pieces;
using UniRx;
using UnityEngine;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenter : IDisposable
    {
        private readonly PieceUseCase _pieceUseCase;
        private readonly GameUseCase _gameUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;
        private readonly CompositeDisposable _disposable = new();

        private IBoardView _view;

        public BoardPresenter(PieceUseCase pieceUseCase, GameUseCase gameUseCase, SelectedPieceRegistry selectedPieceRegistry)
        {
            _pieceUseCase = pieceUseCase;
            _gameUseCase = gameUseCase;
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
                foreach (var destination in destinations)
                {
                    view.SetMovable(destination);
                }
            }).AddTo(_disposable);

            // コマ選択 or 移動先選択
            view.OnClicked.Subscribe(position =>
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
                        _pieceUseCase.UpdatePieces();
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
