﻿using Chess.Domain.ValueObjects;

namespace Chess.Domain.Entities.Pieces
{
    public class Queen : Piece
    {
        public Queen(PlayerColor color, Coordinate position) : base(color, position)
        {
        }

        public override Coordinate[] GetMoveCandidates(Board board) => throw new System.NotImplementedException();
    }
}
