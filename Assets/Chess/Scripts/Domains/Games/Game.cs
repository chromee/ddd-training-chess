using System.Linq;
using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using UniRx;

namespace Chess.Scripts.Domains.Games
{
    public enum GameStatus { InProgress, Check, Checkmate, Stalemate, }

    public class Game
    {
        public Board Board { get; }

        public ISpecialRule[] SpecialRules { get; }

        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

        public Player CurrentTurnPlayer { get; private set; }
        public Player NextTurnPlayer => CurrentTurnPlayer == _whitePlayer ? _blackPlayer : _whitePlayer;

        private readonly ReactiveProperty<GameStatus> _gameStatus = new();
        public IReadOnlyReactiveProperty<GameStatus> GameStatus => _gameStatus;

        public void SwapTurn()
        {
            if (IsCheckmate())
            {
                _gameStatus.Value = Games.GameStatus.Checkmate;
            }
            else if (IsStaleMate())
            {
                _gameStatus.Value = Games.GameStatus.Stalemate;
            }
            else if (IsCheck())
            {
                _gameStatus.Value = Games.GameStatus.Check;
            }
            else
            {
                _gameStatus.Value = Games.GameStatus.InProgress;
            }

            CurrentTurnPlayer = NextTurnPlayer;
        }

        public Game(Board board, Player whitePlayer, Player blackPlayer, ISpecialRule[] specialRules)
        {
            Board = board;

            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;

            SpecialRules = specialRules;

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

        public bool IsStaleMate()
        {
            var pieces = Board.GetPieces(NextTurnPlayer);

            foreach (var piece in pieces)
            {
                var destinations = piece.MoveCandidates(Board);
                foreach (var destination in destinations)
                {
                    var cloneBoard = Board.Clone();
                    var clonePiece = cloneBoard.GetPiece(piece.Position);
                    cloneBoard.MovePiece(clonePiece.Position, destination);
                    if (!cloneBoard.IsCheck(CurrentTurnPlayer)) return false;
                }
            }

            return true;
        }
    }
}
