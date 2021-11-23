using System.Linq;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Boards
{
    public class BoardStatusSolver
    {
        private readonly Game _game;

        public BoardStatusSolver(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// 指定したコマを敵コマのいずれかがとれるかどうか
        /// </summary>
        internal bool CanPick(Piece targetPiece)
        {
            var enemies = _game.Board.GetEnemies(targetPiece);
            var enemiesDestinations = enemies.SelectMany(piece => _game.PieceMovementSolver.MoveCandidates(piece));
            return enemiesDestinations.Any(pos => pos == targetPiece.Position);
        }

        /// <summary>
        /// 指定したコマがとられないように回避できるかどうか
        /// </summary>
        internal bool CanAvoid(Piece avoider)
        {
            var avoidDestinations = _game.PieceMovementSolver.MoveCandidates(avoider);
            if (!CanPick(avoider)) return true;

            foreach (var destination in avoidDestinations)
            {
                var cloneGame = _game.Clone();
                var cloneAvoider = cloneGame.Board.GetPiece(avoider.Position);
                cloneGame.Board.MovePiece(cloneAvoider.Position, destination);
                if (!cloneGame.BoardStatusSolver.CanPick(cloneAvoider)) return true;
            }

            return false;
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがとれるかどうか
        /// </summary>
        internal bool CanKill(Piece target, Piece[] killers)
        {
            return killers.Any(v => _game.PieceMovementSolver.MoveCandidates(v).Any(pos => pos == target.Position));
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがブロックするなりして守れるかどうか
        /// </summary>
        internal bool CanProtect(Piece protectedTarget, Piece[] protectors)
        {
            foreach (var protector in protectors)
            {
                var cloneGame = _game.Clone();
                var cloneProtector = cloneGame.Board.GetPiece(protector.Position);
                var cloneTarget = cloneGame.Board.GetPiece(protectedTarget.Position);

                foreach (var destination in cloneGame.PieceMovementSolver.MoveCandidates(cloneProtector))
                {
                    cloneGame.Board.MovePiece(cloneProtector.Position, destination);
                    if (!cloneGame.BoardStatusSolver.CanPick(cloneTarget)) return true;
                }
            }

            return false;
        }
    }
}
