using System;
using System.Collections.Generic;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Entities.Pieces
{
    public class King : Piece
    {
        public King(PlayerColor color, Coordinate position) : base(color, position)
        {
        }

        public override Coordinate[] GetMoveCandidates(Board board)
        {
            var candidates = new List<Coordinate>();

            for (var c = -1; c <= 1; c++)
            for (var r = -1; r <= 1; r++)
            {
                try
                {
                    candidates.Add(Position + new Coordinate(c, r));
                }
                catch (ArgumentOutOfRangeException e) { }
            }

            return candidates.ToArray();
        }
    }
}
