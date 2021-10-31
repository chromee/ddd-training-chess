using System;
using Chess.Scripts.Applications.Boards;
using Chess.Scripts.Applications.Game;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements;
using Chess.Scripts.Domains.Pieces;
using Chess.Scripts.Presentations.Boards;
using Chess.Scripts.Presentations.Pieces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chess.Scripts.Presentations.LifetimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ChessViewPrefabData _chessViewPrefabData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameFactory>(Lifetime.Scoped).As<IGameFactory>();
            builder.Register<MoveFactory>(Lifetime.Scoped);
            builder.Register<MoveService>(Lifetime.Scoped);
            builder.Register<PieceFactory>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameInitializer>();
            builder.Register<GameRegistry>(Lifetime.Scoped);
            builder.Register<PiecesRegistry>(Lifetime.Scoped);
            builder.Register<SelectedPieceRegistry>(Lifetime.Scoped);

            builder.Register<PieceUseCase>(Lifetime.Scoped);
            builder.Register<GameUseCase>(Lifetime.Scoped);

            builder.Register<BoardViewFactory>(Lifetime.Scoped).As<IBoardViewFactory>();
            builder.Register<PieceViewFactory>(Lifetime.Scoped).As<IPieceViewFactory>();
            builder.Register<BoardPresenter>(Lifetime.Scoped).As<BoardPresenter, IDisposable>();
            builder.RegisterInstance(_chessViewPrefabData);
        }
    }
}
