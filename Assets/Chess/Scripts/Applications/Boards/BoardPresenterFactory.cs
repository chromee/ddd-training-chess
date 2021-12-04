using Chess.Scripts.Applications.Pieces;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenterFactory
    {
        private readonly SelectBoardSquareUseCase _selectBoardSquareUseCase;
        private readonly PieceMoveCandidatesUseCase _pieceMoveCandidatesUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public BoardPresenterFactory(
            PieceMoveCandidatesUseCase pieceMoveCandidatesUseCase,
            SelectBoardSquareUseCase selectBoardSquareUseCase,
            SelectedPieceRegistry selectedPieceRegistry
        )
        {
            _pieceMoveCandidatesUseCase = pieceMoveCandidatesUseCase;
            _selectBoardSquareUseCase = selectBoardSquareUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public BoardPresenter Create(IBoardView view) => new(_pieceMoveCandidatesUseCase, _selectBoardSquareUseCase, _selectedPieceRegistry, view);
    }
}
