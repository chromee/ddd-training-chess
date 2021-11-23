using System;
using System.Collections.Generic;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.Pieces
{
    public class PieceMovementSolver
    {
        private readonly Game _game;

        public PieceMovementSolver(Game game)
        {
            _game = game;
        }

        public Position[] MoveCandidates(Piece piece)
        {
            var candidates = new List<Position>();

            foreach (var move in piece.Moves)
            {
                foreach (var movement in move.Movements)
                {
                    try
                    {
                        var destination = piece.Position + movement;
                        if (!move.CanExecute(_game, piece, destination)) continue;
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
