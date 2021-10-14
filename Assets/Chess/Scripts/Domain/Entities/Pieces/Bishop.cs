using System;
using System.Collections.Generic;
using Chess.Domain.ValueObjects;
using UnityEngine;

namespace Chess.Domain.Entities.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PlayerColor color, Coordinate position) : base(color, position)
        {
        }

        public override Coordinate[] GetMoveCandidates(Board board)
        {
            var candidates = new List<Coordinate>();

            for (var c = 0; c < 8; c++)
            for (var r = 0; r < 8; r++)
            {
                try
                {
                    var target = new Coordinate(c, r);
                    var diff = target - Position;
                    if (Mathf.Abs(diff.Column.Value) == Mathf.Abs(diff.Row.Value)) candidates.Add(target);
                }
                catch (ArgumentOutOfRangeException) { }
            }

            return candidates.ToArray();
        }
    }
}
