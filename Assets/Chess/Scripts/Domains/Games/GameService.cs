using System.Linq;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Games
{
    public class GameService
    {
        public bool IsCheck(Game game, PlayerColor checkPlayer)
        {
            var enemyKing = game.Board.Pieces.FirstOrDefault(v => !v.IsColor(checkPlayer) && v.IsType(PieceType.King));
            return CanPick(game, enemyKing);
        }

        public bool IsCheckmate(Game game, PlayerColor checkmatePlayer)
        {
            var targetKing = game.Board.GetPiece(checkmatePlayer.Opponent(), PieceType.King);
            var protectors = game.Board.Pieces.Where(v => v.IsColor(checkmatePlayer.Opponent()) && v != targetKing).ToArray();

            var killers = game.Board.Pieces.Where(v => v.IsColor(game.CurrentTurnPlayer)).ToArray();
            var killersMoveMap = killers.ToDictionary(v => v, piece => piece.MoveCandidates(game));
            var checkingPiece = killersMoveMap.FirstOrDefault(v => v.Value.Any(pos => pos == targetKing.Position)).Key;

            if (checkingPiece == null) return false;

            // キングがよけれるかどうか
            if (CanAvoid(game, targetKing)) return false;

            // 他のコマがチェックしてるコマを殺せるかどうか
            if (CanKill(game, checkingPiece, protectors)) return false;

            // 他のコマがブロックできるorチェックしてるコマを殺せるかかどうか
            if (CanProtect(game, targetKing, protectors)) return false;

            return true;
        }

        public bool IsStaleMate(Game game, PlayerColor movePlayer)
        {
            var pieces = game.Board.GetPieces(movePlayer);

            foreach (var piece in pieces)
            {
                var destinations = piece.MoveCandidates(game);
                foreach (var destination in destinations)
                {
                    var cloneGame = game.Clone();
                    var clonePiece = cloneGame.Board.GetPiece(piece.Position);
                    cloneGame.Board.MovePiece(clonePiece.Position, destination);
                    if (!IsCheck(cloneGame, cloneGame.CurrentTurnPlayer)) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 指定したコマを敵コマのいずれかがとれるかどうか
        /// </summary>
        public bool CanPick(Game game, Piece targetPiece)
        {
            var enemies = game.Board.GetEnemies(targetPiece);
            var enemiesDestinations = enemies.SelectMany(piece => piece.MoveCandidates(game));
            return enemiesDestinations.Any(pos => pos == targetPiece.Position);
        }

        /// <summary>
        /// 指定したコマがとられないように回避できるかどうか
        /// </summary>
        public bool CanAvoid(Game game, Piece avoider)
        {
            var avoidDestinations = avoider.MoveCandidates(game);
            if (!CanPick(game, avoider)) return true;

            foreach (var destination in avoidDestinations)
            {
                var cloneGame = game.Clone();
                var cloneAvoider = cloneGame.Board.GetPiece(avoider.Position);
                cloneGame.Board.MovePiece(cloneAvoider.Position, destination);
                if (!CanPick(cloneGame, cloneAvoider)) return true;
            }

            return false;
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがとれるかどうか
        /// </summary>
        public bool CanKill(Game game, Piece target, Piece[] killers)
        {
            return killers.Any(v => v.MoveCandidates(game).Any(pos => pos == target.Position));
        }

        /// <summary>
        /// 指定したコマを指定したコマたちがブロックするなりして守れるかどうか
        /// </summary>
        public bool CanProtect(Game game, Piece protectedTarget, Piece[] protectors)
        {
            foreach (var protector in protectors)
            {
                var cloneGame = game.Clone();
                var cloneProtector = cloneGame.Board.GetPiece(protector.Position);
                var cloneTarget = cloneGame.Board.GetPiece(protectedTarget.Position);

                foreach (var destination in cloneProtector.MoveCandidates(cloneGame))
                {
                    cloneGame.Board.MovePiece(cloneProtector.Position, destination);
                    if (!CanPick(cloneGame, cloneTarget)) return true;
                }
            }

            return false;
        }
    }
}
