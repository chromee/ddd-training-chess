﻿using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnDiagonalMovementCondition : IMovementCondition
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            var destPiece = game.Board.GetPiece(destination);
            return destPiece != null && destPiece.IsOpponent(piece);
        }
    }
}
