using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.HandLogs;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;

namespace Chess.Scripts.Domains.Games
{
    public class Game
    {
        public Board Board { get; }

        public SpecialRule[] SpecialRules { get; private set; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

        public Player CurrentTurnPlayer { get; private set; }
        public Player NextTurnPlayer => CurrentTurnPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
        public void SwapTurn() => CurrentTurnPlayer = NextTurnPlayer;

        public Game(Board board, Player whitePlayer, Player blackPlayer)
        {
            Board = board;

            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;

            SpecialRules = new[]
            {
                new EnPassant(),
            };

            // 先行は白プレイヤー
            CurrentTurnPlayer = whitePlayer;
        }

        public bool IsCheck()
        {
            return Board.IsCheck(CurrentTurnPlayer);
        }

        public bool IsCheckmate()
        {
            var targetKing = Board.GetPiece(NextTurnPlayer, PieceType.King);
            var protectors = Board.Pieces.Where(v => v.IsOwner(NextTurnPlayer) && v != targetKing).ToArray();

            var killers = Board.Pieces.Where(v => v.IsOwner(CurrentTurnPlayer)).ToArray();
            var killersMoveMap = killers.ToDictionary(v => v, piece => piece.MoveCandidates(Board));
            var checkingPiece = killersMoveMap.FirstOrDefault(v => v.Value.Any(pos => pos == targetKing.Position)).Key;

            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            if (Board.CanAvoid(targetKing)) return false;

            // 他のコマがチェックしてるコマを殺せるかどうか
            if (Board.CanKill(checkingPiece, protectors)) return false;

            // 他のコマがブロックできるorチェックしてるコマを殺せるかかどうか
            if (Board.CanProtect(targetKing, protectors)) return false;

            return true;
        }
    }
}
