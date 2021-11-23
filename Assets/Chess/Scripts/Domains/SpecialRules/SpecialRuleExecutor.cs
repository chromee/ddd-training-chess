using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class SpecialRuleExecutor
    {
        private readonly ISpecialRule[] _specialRules;

        public SpecialRuleExecutor(ISpecialRule[] specialRules)
        {
            _specialRules = specialRules;
        }

        public void TryExecute(Game game)
        {
            foreach (var specialRule in _specialRules) specialRule.TryExecute(game);
        }
    }
}
