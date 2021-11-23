using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Logger;
using Chess.Scripts.Domains.Movements;

namespace Chess.Scripts.Domains.Games
{
    public class Game
    {
        public Board Board { get; }
        public PieceMovementLogger Logger { get; }
        public GameStatusHandler StatusHandler { get; }
        public PieceMovementCandidatesCalculator PieceMovementCandidatesCalculator { get; }

        public PlayerColor CurrentTurnPlayer { get; private set; }
        public PlayerColor NextTurnPlayer => CurrentTurnPlayer.Opponent();

        public Game(Board board, PieceMovementLogger logger = null)
        {
            Board = board;
            Logger = logger ?? new PieceMovementLogger();
            StatusHandler = new GameStatusHandler(this);
            PieceMovementCandidatesCalculator = new PieceMovementCandidatesCalculator(this);

            // 先行は白プレイヤー
            CurrentTurnPlayer = PlayerColor.White;
        }

        public void SwapTurn()
        {
            StatusHandler.UpdateStatus();
            CurrentTurnPlayer = NextTurnPlayer;
        }

        public Game Clone()
        {
            var cloneBoard = Board.Clone();
            var logger = Logger.Clone();
            return new Game(cloneBoard, logger);
        }
    }
}
