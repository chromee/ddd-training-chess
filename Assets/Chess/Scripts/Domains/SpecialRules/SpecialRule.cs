using Chess.Scripts.Domains.Boards;

namespace Chess.Scripts.Domains.SpecialRules
{
    public abstract class SpecialRule
    {
        public abstract void TryExecute(Board board);
    }
}
