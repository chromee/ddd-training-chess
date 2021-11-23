using Chess.Scripts.Applications.Pieces;

namespace Chess.Scripts.Applications.Boards
{
    public class BoardPresenterFactory
    {
        private readonly BoardUseCase _boardUseCase;
        private readonly PieceMoveCandidatesUseCase _pieceMoveCandidatesUseCase;
        private readonly SelectedPieceRegistry _selectedPieceRegistry;

        public BoardPresenterFactory(
            PieceMoveCandidatesUseCase pieceMoveCandidatesUseCase,
            BoardUseCase boardUseCase,
            SelectedPieceRegistry selectedPieceRegistry
        )
        {
            _pieceMoveCandidatesUseCase = pieceMoveCandidatesUseCase;
            _boardUseCase = boardUseCase;
            _selectedPieceRegistry = selectedPieceRegistry;
        }

        public BoardPresenter Create(IBoardView view) => new(_pieceMoveCandidatesUseCase, _boardUseCase, _selectedPieceRegistry, view);
    }
}
