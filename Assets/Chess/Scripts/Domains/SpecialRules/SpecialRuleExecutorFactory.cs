namespace Chess.Scripts.Domains.SpecialRules
{
    public class SpecialRuleExecutorFactory
    {
        private readonly PromotionNotifier _notifier;

        public SpecialRuleExecutorFactory(PromotionNotifier notifier)
        {
            _notifier = notifier;
        }

        public SpecialRuleExecutor Create()
        {
            var specialRules = new ISpecialRule[]
            {
                new EnPassant(),
                new Castling(),
                new Promotion(_notifier),
            };

            return new SpecialRuleExecutor(specialRules);
        }
    }
}
