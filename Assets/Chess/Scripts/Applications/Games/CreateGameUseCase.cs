using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Applications.SpecialRules;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Applications.Games
{
    public class CreateGameUseCase
    {
        private readonly BoardPresenterFactory _boardPresenterFactory;
        private readonly IBoardViewFactory _boardViewFactory;
        private readonly GameFactory _gameFactory;
        private readonly GamePresenterFactory _gamePresenterFactory;
        private readonly GameRegistry _gameRegistry;
        private readonly IPieceViewFactory _pieceViewFactory;
        private readonly PromotionPresenterFactory _promotionPresenterFactory;

        private CompositeDisposable _disposable;

        public CreateGameUseCase(GameFactory gameFactory, GameRegistry gameRegistry,
            BoardPresenterFactory boardPresenterFactory, PromotionPresenterFactory promotionPresenterFactory,
            GamePresenterFactory gamePresenterFactory, IBoardViewFactory boardViewFactory,
            IPieceViewFactory pieceViewFactory)
        {
            _gameFactory = gameFactory;
            _gameRegistry = gameRegistry;
            _boardPresenterFactory = boardPresenterFactory;
            _promotionPresenterFactory = promotionPresenterFactory;
            _gamePresenterFactory = gamePresenterFactory;
            _boardViewFactory = boardViewFactory;
            _pieceViewFactory = pieceViewFactory;
        }

        public void Execute()
        {
            _disposable?.Dispose();
            _disposable = new CompositeDisposable();

            var game = _gameFactory.CreateBasicGame();
            _gameRegistry.Register(game);

            var boardView = _boardViewFactory.CreateBoardView();
            boardView.AddTo(_disposable);
            _boardPresenterFactory.Create(boardView).AddTo(_disposable);

            foreach (var piece in game.Board.Pieces) BindPiece(piece, _disposable);
            game.Board.Pieces.ObserveAdd().Subscribe(v => BindPiece(v.Value, _disposable)).AddTo(_disposable);

            _promotionPresenterFactory.Create(game.Board).AddTo(_disposable);

            _gamePresenterFactory.Create(this, game).AddTo(_disposable);
        }

        private void BindPiece(Piece piece, CompositeDisposable disposable)
        {
            var view = _pieceViewFactory.CreatePieceView(piece.ToToDto());
            view.AddTo(disposable);
            new PiecePresenter(piece, view).AddTo(disposable);
        }
    }
}
