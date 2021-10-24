﻿using System.Collections.Generic;
using Chess.Domain.Boards;
using Chess.Domain.Movements;

namespace Chess.Domain.Pieces
{
    public class PieceFactory
    {
        private readonly MoveFactory _moveFactory;

        public PieceFactory()
        {
            _moveFactory = new MoveFactory();
        }

        public Piece CreatePawn(Player player, Position position)
        {
            return new Piece(player, PieceType.Pawn, position, _moveFactory.CreatePawnMove());
        }

        public Piece CreateKnight(Player player, Position position)
        {
            return new Piece(player, PieceType.Knight, position, _moveFactory.CreateKnightMove());
        }

        public Piece CreateRook(Player player, Position position)
        {
            return new Piece(player, PieceType.Rook, position, _moveFactory.CreateRookMove());
        }

        public Piece CreateBishop(Player player, Position position)
        {
            return new Piece(player, PieceType.Bishop, position, _moveFactory.CreateBishopMove());
        }

        public Piece CreateQueen(Player player, Position position)
        {
            return new Piece(player, PieceType.Queen, position, _moveFactory.CreateQueenMove());
        }

        public Piece CreateKing(Player player, Position position)
        {
            return new Piece(player, PieceType.King, position, _moveFactory.CreateKingMove());
        }
    }
}
