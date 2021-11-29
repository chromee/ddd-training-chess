using System.Collections.Generic;
using System.Linq;
using Chess.Scripts.Domains.Boards;

namespace Chess.Scripts.Domains.Logger
{
    public class BoardLogger
    {
        private readonly List<BoardLog> _boardLogs;

        public BoardLogger(List<BoardLog> boardLogs = null)
        {
            _boardLogs = boardLogs != null ? new List<BoardLog>(boardLogs) : new List<BoardLog>();
        }

        public IReadOnlyList<BoardLog> AllLogs => _boardLogs;

        public void AddLog(Board board)
        {
            var pieceLogs = board.Pieces.Select(v => new PieceLog(v.Color, v.Type, v.Position)).ToArray();
            _boardLogs.Add(new BoardLog(pieceLogs));
        }

        public BoardLogger Clone() => new(_boardLogs);
    }
}
