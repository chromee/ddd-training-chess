using System.Collections.Generic;
using System.Linq;

namespace Chess.Scripts.Domains.Logger
{
    public class PieceMovementLogger
    {
        private readonly List<PieceMovementLog> _pieceMovementLogs;

        public PieceMovementLogger(List<PieceMovementLog> pieceMovementLog = null)
        {
            _pieceMovementLogs = pieceMovementLog != null ? new List<PieceMovementLog>(pieceMovementLog) : new List<PieceMovementLog>();
        }

        public IReadOnlyList<PieceMovementLog> AllLogs => _pieceMovementLogs;
        public PieceMovementLog? LastPieceMovement => _pieceMovementLogs.Count == 0 ? null : _pieceMovementLogs.Last();
        public PieceMovementLog? SecondLastPieceMovement => _pieceMovementLogs.Count <= 1 ? null : _pieceMovementLogs[^2];

        public void AddLog(PieceMovementLog log) => _pieceMovementLogs.Add(log);

        public PieceMovementLogger Clone() => new(_pieceMovementLogs);
    }
}
