using System;
using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Games;
using Chess.Scripts.Applications.Messages;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Applications.SpecialRules;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Domains.SpecialRules;
using Chess.Scripts.Presentations.Boards;
using Chess.Scripts.Presentations.Messages;
using Chess.Scripts.Presentations.Pieces;
using Chess.Scripts.Presentations.SpecialRules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chess.Scripts.Presentations.LifetimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ChessViewPrefabData _chessViewPrefabData;
        [SerializeField] private MessageView _messageView;
        [SerializeField] private PromotionView _promotionView;
        [SerializeField] private GameResultView _gameResultView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameFactory>(Lifetime.Scoped);
            builder.Register<PieceFactory>(Lifetime.Scoped);
            builder.Register<MoveService>(Lifetime.Scoped);
            builder.Register<SpecialRuleService>(Lifetime.Scoped);
            builder.Register<PromotionNotifier>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameInitializer>();
            builder.Register<GameRegistry>(Lifetime.Scoped);
            builder.Register<SelectedPieceRegistry>(Lifetime.Scoped);

            builder.Register<PieceUseCase>(Lifetime.Scoped);
            builder.Register<BoardUseCase>(Lifetime.Scoped);
            builder.Register<GameUseCase>(Lifetime.Scoped);

            builder.RegisterInstance(_chessViewPrefabData);

            builder.Register<GamePresenterFactory>(Lifetime.Scoped);

            builder.RegisterInstance(_gameResultView).As<IGameResultView>();
            builder.RegisterInstance(_messageView).As<IMessagePublisher>();

            builder.Register<BoardViewFactory>(Lifetime.Scoped).As<IBoardViewFactory>();
            builder.Register<BoardPresenterFactory>(Lifetime.Scoped);

            builder.Register<PieceViewFactory>(Lifetime.Scoped).As<IPieceViewFactory>();

            builder.Register<PromotionPresenterFactory>(Lifetime.Scoped);
            builder.RegisterInstance(_promotionView).As<IPromotionView>();
        }
    }
}
