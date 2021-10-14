using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Entities;
using Chess.Domain.ValueObjects;

namespace Chess.Domain.Service
{
    public class PieceMovementService
    {
        public Coordinate[] GetLinearlyMove(Coordinate position, Board board)
        {
            var candidates = new List<Coordinate>();
            return candidates.ToArray();
        }

        public Coordinate[] GetDiagonallyMove(Coordinate position, Board board)
        {
            var candidates = new List<Coordinate>();

            // 右上
            for (var v = 1; v < 8; v++)
            {
                try
                {
                    var target = position + new Coordinate(v, v);
                    candidates.Add(target);
                    if (board.Pieces.Any(p => p.Position == target)) break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            // 左上
            for (var v = 1; v < 8; v++)
            {
                try
                {
                    var target = position + new Coordinate(-v, v);
                    candidates.Add(target);
                    if (board.Pieces.Any(p => p.Position == target)) break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            // 右下
            for (var v = 1; v < 8; v++)
            {
                try
                {
                    var target = position + new Coordinate(-v, -v);
                    candidates.Add(target);
                    if (board.Pieces.Any(p => p.Position == target)) break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            // 左下
            for (var v = 1; v < 8; v++)
            {
                try
                {
                    var target = position + new Coordinate(v, -v);
                    candidates.Add(target);
                    if (board.Pieces.Any(p => p.Position == target)) break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            return candidates.ToArray();
        }
    }
}
