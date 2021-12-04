using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using NUnit.Framework;

namespace Chess.Scripts.Applications.Tests
{
    public class ApplicationTestBase
    {
        protected SelectBoardSquareUseCase SelectBoardSquareUseCase;
        protected GameFactory GameFactory;
        protected GameRegistry GameRegistry;
        protected MockMessagePublisher MessagePublisher;
        protected PieceFactory PieceFactory;
        protected PieceMoveCandidatesUseCase PieceMoveCandidatesUseCase;
        protected PieceMovementExecutor PieceMovementExecutor;
        protected MovePieceUseCase MovePieceUseCase;
        protected PromotionExecutor PromotionExecutor;
        protected PromotionNotifier PromotionNotifier;
        protected SelectedPieceRegistry SelectedPieceRegistry;
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

            MovePieceUseCase = new MovePieceUseCase(GameRegistry, SelectedPieceRegistry, PieceMovementExecutor, MessagePublisher);
            PieceMoveCandidatesUseCase = new PieceMoveCandidatesUseCase(GameRegistry);
            SelectPieceUseCase = new SelectPieceUseCase(GameRegistry, SelectedPieceRegistry);
            SelectBoardSquareUseCase = new SelectBoardSquareUseCase(SelectPieceUseCase, MovePieceUseCase, SelectedPieceRegistry);
        }
    }
}
