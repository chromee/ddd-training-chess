﻿using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Conditions
{
    public class PawnDoubleMoveConditions : IMoveConditions
    {
        public bool CanExecute(Game game, Piece piece, Position destination)
        {
            // 目的地にコマがあると移動できない
            if (game.Board.ExistPiece(destination)) return false;

            // 初期位置でなければ移動できない
            var initialY = piece.IsWhite() ? 1 : 6;
            if (piece.Position.Y != initialY) return false;

            // 間にコマがあると移動できない
            var blockY = piece.IsWhite() ? 2 : 5;
            if (game.Board.ExistPiece(new Position(destination.X, blockY))) return false;

            return true;
        }
    }
}
