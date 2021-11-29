using System.Collections.Generic;
using System.Linq;

namespace Chess.Scripts.Domains.Logger
{
    public class PieceMovementLogger
    {
        private readonly List<PieceMovementLog> _pieceMovementLog;

        public PieceMovementLogger(List<PieceMovementLog> pieceMovementLog = null)
        {
            _pieceMovementLog = pieceMovementLog ?? new List<PieceMovementLog>();
        }

        public IReadOnlyList<PieceMovementLog> AllLog => _pieceMovementLog;
        public PieceMovementLog? LastPieceMovement => _pieceMovementLog.Count == 0 ? null : _pieceMovementLog.Last();
        public PieceMovementLog? SecondLastPieceMovement => _pieceMovementLog.Count <= 1 ? null : _pieceMovementLog[^2];

        public void AddLog(PieceMovementLog log) => _pieceMovementLog.Add(log);

        public PieceMovementLogger Clone() => new(_pieceMovementLog);
    }
}
