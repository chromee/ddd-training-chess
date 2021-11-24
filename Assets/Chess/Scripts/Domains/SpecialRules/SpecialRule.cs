using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public abstract class SpecialRule
    {
        public abstract void TryExecute(Game game);
    }
}
