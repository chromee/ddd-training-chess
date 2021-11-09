using System;
using System.Collections.Generic;
using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Applications.SpecialRules;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using UniRx;
using VContainer.Unity;

namespace Chess.Scripts.Applications.Games
{
    public class GameInitializer : IInitializable, IDisposable
    {
        private readonly GameFactory _gameFactory;
        private readonly GameRegistry _gameRegistry;
        private readonly IBoardViewFactory _boardViewFactory;
        private readonly IPieceViewFactory _pieceViewFactory;
        private readonly BoardPresenter _boardPresenter;
        private readonly PromotionPresenter _promotionPresenter;
        private readonly GamePresenter _gamePresenter;

        private readonly List<IDisposable> _disposables = new();

        public GameInitializer(
            GameFactory gameFactory,
            GameRegistry gameRegistry,
            IBoardViewFactory boardViewFactory,
            IPieceViewFactory pieceViewFactory,
            BoardPresenter boardPresenter,
            GamePresenter gamePresenter,
            PromotionPresenter promotionPresenter)
        {
            _gameFactory = gameFactory;
            _gameRegistry = gameRegistry;
            _boardViewFactory = boardViewFactory;
            _pieceViewFactory = pieceViewFactory;
            _boardPresenter = boardPresenter;
            _gamePresenter = gamePresenter;
            _promotionPresenter = promotionPresenter;
        }


        public void Initialize()
        {
            var game = _gameFactory.CreateGame();
            _gameRegistry.Register(game);

            _boardPresenter.Bind(_boardViewFactory.CreateBoardView());

            foreach (var piece in game.Board.Pieces) BindPiece(piece);
            _disposables.Add(game.Board.Pieces.ObserveAdd().Subscribe(v => BindPiece(v.Value)));

            _promotionPresenter.Bind(game.Board);

            _gamePresenter.Bind(game);
        }

        private void BindPiece(Piece piece)
        {
            var view = _pieceViewFactory.CreatePieceView(piece.ToData());
            var presenter = new PiecePresenter(piece, view);
            _disposables.Add(presenter);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables) disposable.Dispose();
        }
    }
}
