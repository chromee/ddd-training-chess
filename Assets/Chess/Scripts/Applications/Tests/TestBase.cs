using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;

namespace Chess.Scripts.Applications.Tests
{
    public class TestBase
    {
        protected GameFactory GameFactory;
        protected PieceFactory PieceFactory;
        protected PieceMovementExecutor PieceMovementExecutor;
        protected PromotionExecutor PromotionExecutor;
        protected PromotionNotifier PromotionNotifier;

        protected GameRegistry GameRegistry;
        protected SelectedPieceRegistry SelectedPieceRegistry;
        protected MockMessagePublisher MessagePublisher;

        protected BoardUseCase BoardUseCase;
        protected PieceMoveUseCase PieceMoveUseCase;
        protected PieceMoveCandidatesUseCase PieceMoveCandidatesUseCase;
        protected SelectPieceUseCase SelectPieceUseCase;

        [SetUp]
        public void Install()
        {
            InstallDomains();
            InstallApplications();
        }

        private void InstallDomains()
        {
            PieceFactory = new PieceFactory();

            PromotionNotifier = new PromotionNotifier();
            PromotionExecutor = new PromotionExecutor(PromotionNotifier, PieceFactory);
            var specialRuleExecutorFactory = new SpecialRuleExecutorFactory(PromotionNotifier);

            GameFactory = new GameFactory(PieceFactory, specialRuleExecutorFactory);

            PieceMovementExecutor = new PieceMovementExecutor();
        }

        private void InstallApplications()
        {
            GameRegistry = new GameRegistry();
            SelectedPieceRegistry = new SelectedPieceRegistry();
            MessagePublisher = new MockMessagePublisher();

            PieceMoveUseCase = new PieceMoveUseCase(GameRegistry, SelectedPieceRegistry, PieceMovementExecutor, MessagePublisher);
            PieceMoveCandidatesUseCase = new PieceMoveCandidatesUseCase(GameRegistry);
            SelectPieceUseCase = new SelectPieceUseCase(GameRegistry, SelectedPieceRegistry);
            BoardUseCase = new BoardUseCase(SelectPieceUseCase, PieceMoveUseCase, SelectedPieceRegistry);
        }
    }
}
