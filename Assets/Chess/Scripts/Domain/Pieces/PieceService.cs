using System;
using System.Collections.Generic;

namespace Chess.Domain.Pieces
{
    public class PieceService
    {
        public Position[] MoveCandidates(Piece piece, Board board)
        {
            var candidates = new List<Position>();

            foreach (var move in piece.Moves)
            {
                foreach (var relativeDest in move.Destinations)
                {
                    try
                    {
                        var destination = piece.Position + relativeDest;
                        if (!move.Conditions.CanExecute(piece, destination, board)) continue;
                        candidates.Add(destination);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Destinationsの中身が複数あるのはInfinityMoveだけなので、
                        // 範囲外になったら(Positionのコンストラクタでthrowされたら)break
                        break;
                    }
                }
            }

            return candidates.ToArray();
        }
    }
}
