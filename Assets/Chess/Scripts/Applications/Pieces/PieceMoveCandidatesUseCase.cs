using System;
using System.Linq;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Domains.Pieces;
using UnityEngine;

namespace Chess.Scripts.Applications.Pieces
{
    public class PieceMoveCandidatesUseCase
    {
        private readonly GameRegistry _gameRegistry;

        public PieceMoveCandidatesUseCase(GameRegistry gameRegistry)
        {
            _gameRegistry = gameRegistry;
        }

        public Vector2Int[] Get(Piece piece)
        {
            if (piece == null) throw new Exception("not found selected piece");
            return _gameRegistry.CurrentGame.PieceMovementSolver.MoveCandidates(piece).Select(v => v.ToVector2()).ToArray();
        }
    }
}
