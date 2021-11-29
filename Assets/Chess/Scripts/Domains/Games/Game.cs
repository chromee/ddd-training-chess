using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Logger;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;

namespace Chess.Scripts.Domains.Games
{
    public class Game
    {
        private readonly ReactiveProperty<GameStatus> _gameStatus = new();

        public Game(Board board, SpecialRuleExecutor specialRuleExecutor,
            PieceMovementLogger pieceMovementLogger = null, BoardLogger boardLogger = null)
        {
            Board = board;
            BoardStatusSolver = new BoardStatusSolver(this);
            StatusSolver = new GameStatusSolver(this);
            SpecialRuleExecutor = specialRuleExecutor;
            PieceMovementLogger = pieceMovementLogger ?? new PieceMovementLogger();
            BoardLogger = boardLogger ?? new BoardLogger();
            PieceMovementSolver = new PieceMovementSolver(this);

            // 先行は白プレイヤー
            CurrentTurnPlayer = PlayerColor.White;
        }

        public Board Board { get; }
        public BoardStatusSolver BoardStatusSolver { get; }
        public GameStatusSolver StatusSolver { get; }
        public SpecialRuleExecutor SpecialRuleExecutor { get; }
        public PieceMovementLogger PieceMovementLogger { get; }
        public BoardLogger BoardLogger { get; }
        public PieceMovementSolver PieceMovementSolver { get; }

        public PlayerColor CurrentTurnPlayer { get; private set; }
        public PlayerColor NextTurnPlayer => CurrentTurnPlayer.Opponent();
        public GameStatus CurrentStatus => _gameStatus.Value;
        public IReadOnlyReactiveProperty<GameStatus> CurrentStatusObservable => _gameStatus;

        public void SwapTurn()
        {
            _gameStatus.Value = StatusSolver.SolveStatus();
            CurrentTurnPlayer = NextTurnPlayer;
        }

        public Game Clone()
        {
            var cloneBoard = Board.Clone();
            var pieceMovementLogger = PieceMovementLogger.Clone();
            var boardLogger = BoardLogger.Clone();
            return new Game(cloneBoard, SpecialRuleExecutor, pieceMovementLogger, boardLogger);
        }
    }
}
