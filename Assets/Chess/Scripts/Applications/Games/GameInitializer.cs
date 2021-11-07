using System;
using System.Collections.Generic;
using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Games;
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

        private readonly List<IDisposable> _disposables = new();

        public GameInitializer(
            GameFactory gameFactory,
            GameRegistry gameRegistry,
            IBoardViewFactory boardViewFactory,
            IPieceViewFactory pieceViewFactory,
            BoardPresenter boardPresenter
        )
        {
            _gameFactory = gameFactory;
            _gameRegistry = gameRegistry;
            _boardViewFactory = boardViewFactory;
            _pieceViewFactory = pieceViewFactory;
            _boardPresenter = boardPresenter;
        }


        public void Initialize()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            var game = _gameFactory.CreateGame();
            _gameRegistry.Register(game);

            var boardView = _boardViewFactory.CreateBoardView();
            _boardPresenter.Bind(boardView);

            foreach (var piece in game.Board.Pieces)
            {
                var presenter = new PiecePresenter(piece, _pieceViewFactory.CreatePieceView(piece.ToData()));
                _disposables.Add(presenter);
            }

            // 主にプロモーションで生成されたコマをViewに反映する
            var d = game.Board.Pieces.ObserveAdd().Subscribe(v =>
            {
                var piece = v.Value;
                var presenter = new PiecePresenter(piece, _pieceViewFactory.CreatePieceView(piece.ToData()));
                _disposables.Add(presenter);
            });
            _disposables.Add(d);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
