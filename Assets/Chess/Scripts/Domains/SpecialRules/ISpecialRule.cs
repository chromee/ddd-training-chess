using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public interface ISpecialRule
    {
        void TryExecute(Game game);
    }
}
