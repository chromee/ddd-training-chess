using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.HandLogs;

namespace Chess.Scripts.Domains.SpecialRules
{
    public abstract class SpecialRule
    {
        public abstract void TryExecute(Board board);
    }
}
