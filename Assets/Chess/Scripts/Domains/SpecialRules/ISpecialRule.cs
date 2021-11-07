using Chess.Scripts.Domains.Boards;

namespace Chess.Scripts.Domains.SpecialRules
{
    public interface ISpecialRule
    {
        void TryExecute(Board board);
    }
}
