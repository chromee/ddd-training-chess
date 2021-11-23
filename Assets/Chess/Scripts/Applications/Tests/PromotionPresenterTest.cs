using System;
using System.Linq;
using Chess.Scripts.Applications.SpecialRules;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;
using NUnit.Framework;
using UniRx;
using PieceType = Chess.Scripts.Applications.Pieces.PieceType;

namespace Chess.Scripts.Applications.Tests
{
    public class PromotionPresenterTest : TestBase
    {
        [Test]
        public void プロモーション時にダイアログを表示()
        {
            // □ □ □ □ ☆ □ □ □
            // □ □ □ □ □ □ ● □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □
            //          ↓
            // □ □ □ □ ☆ □ ● □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ □ □ □ □
            // □ □ □ □ ★ □ □ □

            var whitePawn = PieceFactory.CreatePawn(PlayerColor.White, new Position(1, 6));
            var whitePieces = new[]
            {
                whitePawn,
                PieceFactory.CreateKing(PlayerColor.White, new Position(3, 1)),
            };
            var blackPieces = new[]
            {
                PieceFactory.CreateKing(PlayerColor.Black, new Position(3, 7)),
            };
            var game = GameFactory.CreateGame(whitePieces.Concat(blackPieces).ToList());

            var view = new PromotionViewMock();
            var presenter = new PromotionPresenter(PromotionNotifier, PromotionExecutor, view, game.Board);

            PieceMovementExecutor.Move(game, whitePawn, new Position(1, 7));

            Assert.IsTrue(view.IsShowDialogue);

            presenter.Dispose();
        }
    }

    public class PromotionViewMock : IPromotionView
    {
        public IObservable<PieceType> OnSelectPieceType => new Subject<PieceType>();
        public bool IsShowDialogue { get; private set; }
        public void ShowPromotionDialogue() => IsShowDialogue = true;
    }
}
