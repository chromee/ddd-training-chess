using Chess.Scripts.Domains.Games;

namespace Chess.Scripts.Domains.SpecialRules
{
    public class SpecialRuleExecutor
    {
        private readonly SpecialRule[] _specialRules;

        public SpecialRuleExecutor(SpecialRule[] specialRules)
        {
            _specialRules = specialRules;
        }

        public void TryExecute(Game game)
        {
            foreach (var specialRule in _specialRules) specialRule.TryExecute(game);
        }
    }
}
