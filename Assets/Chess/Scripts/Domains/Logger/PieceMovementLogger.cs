using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.Logger
{
    public class PieceMovementLogger
    {
        private readonly List<PieceMovementLog?> _pieceMovementLog;

        public PieceMovementLogger(List<PieceMovementLog?> pieceMovementLog = null)
        {
            _pieceMovementLog = pieceMovementLog ?? new List<PieceMovementLog?>();
        }

        public PieceMovementLog? LastPieceMovement => _pieceMovementLog.LastOrDefault();
        public PieceMovementLog? SecondLastPieceMovement => _pieceMovementLog.Count <= 1 ? null : _pieceMovementLog[^2];
        public void AddLog(PieceMovementLog log) => _pieceMovementLog.Add(log);

        public PieceMovementLogger Clone() => new PieceMovementLogger(_pieceMovementLog);
    }
}
