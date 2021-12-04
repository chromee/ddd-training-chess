using System;
using Chess.Scripts.Applications.Pieces;
using UniRx;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public BoardPresenter(
            PieceMoveCandidatesUseCase pieceMoveCandidatesUseCase,
            SelectBoardSquareUseCase selectBoardSquareUseCase,
            SelectedPieceRegistry selectedPieceRegistry,
            IBoardView view
        )
        {
            // 選択中のコマが変わったら移動可能マスの表示を更新
            selectedPieceRegistry.SelectedPiece.Subscribe(piece =>
            {
                view.ResetSquares();

                if (piece == null) return;

                var destinations = pieceMoveCandidatesUseCase.Get(piece);
                foreach (var destination in destinations) view.SetMovable(destination);
            }).AddTo(_disposable);

            // コマ選択 or 移動先選択
            view.OnClicked.Subscribe(position =>
            {
                view.ResetSquares();
                selectBoardSquareUseCase.Execute(position);
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
