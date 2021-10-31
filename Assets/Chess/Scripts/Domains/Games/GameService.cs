// using System.Linq;
// using Chess.Scripts.Domains.Boards;
// using Chess.Scripts.Domains.Pieces;
//
// namespace Chess.Scripts.Domains.Games
// {
//     public class GameService
//     {
//         public bool IsCheck(Board board, Player player)
//         {
//             var enemyKing = board.Pieces.FirstOrDefault(v => !v.IsOwner(player) && v.IsSameType(PieceType.King));
//             return board.CanPick(enemyKing);
//         }
//         
//         public bool IsCheckmate(Game game)
//         {
//             var targetKing = game.Board.GetPiece(game.NextTurnPlayer, PieceType.King);
//             var protectors = Board.Pieces.Where(v => v.IsOwner(game.NextTurnPlayer) && v != targetKing).ToArray();
//
//             var killers = game.Board.Pieces.Where(v => v.IsOwner(game.CurrentTurnPlayer)).ToArray();
//             var killersMoveMap = killers.ToDictionary(v => v, piece => piece.MoveCandidates(Board));
//             var checkingPiece = killersMoveMap.FirstOrDefault(v => v.Value.Any(pos => pos == targetKing.Position)).Key;
//
//             if (checkingPiece == null) return false;
//
//             // キングがよけれるかどうか
//             if (game.Board.CanAvoid(targetKing)) return false;
//
//             // 他のコマがチェックしてるコマを殺せるかどうか
//             if (game.Board.CanKill(checkingPiece, protectors)) return false;
//
//             // 他のコマがブロックできるorチェックしてるコマを殺せるかかどうか
//             if (game.Board.CanProtect(targetKing, protectors)) return false;
//
//             return true;
//         }
//     }
// }
