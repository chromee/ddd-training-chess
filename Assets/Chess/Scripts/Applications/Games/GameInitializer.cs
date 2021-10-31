using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Games;
using VContainer.Unity;

namespace Chess.Scripts.Applications.Games
{
    public class GameInitializer : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly GameRegistry _gameRegistry;
        private readonly PiecesRegistry _piecesRegistry;
        private readonly IBoardViewFactory _boardViewFactory;
        private readonly IPieceViewFactory _pieceViewFactory;
        private readonly BoardPresenter _boardPresenter;

        public GameInitializer(
            IGameFactory gameFactory,
            GameRegistry gameRegistry,
            IBoardViewFactory boardViewFactory,
            IPieceViewFactory pieceViewFactory,
            BoardPresenter boardPresenter,
            PiecesRegistry piecesRegistry
        )
        {
            _gameFactory = gameFactory;
            _gameRegistry = gameRegistry;
            _boardViewFactory = boardViewFactory;
            _pieceViewFactory = pieceViewFactory;
            _boardPresenter = boardPresenter;
            _piecesRegistry = piecesRegistry;
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
                _piecesRegistry.AddPiece(presenter);
            }
        }
    }
}
