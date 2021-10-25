using System;
using Chess.Application.interfaces;
using Chess.Application.Services;
using Chess.Application.UseCase;
using Chess.Domain;
using Chess.Domain.Games;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;
using Chess.View.Factories;
using Chess.View.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chess.Infrastructure.VContainer
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
            builder.Register<GameRegistry>(Lifetime.Singleton);
            builder.Register<SelectedPieceRegistry>(Lifetime.Singleton);

            builder.Register<PieceUseCase>(Lifetime.Scoped);
            builder.Register<GameUseCase>(Lifetime.Scoped);

            builder.Register<BoardViewFactory>(Lifetime.Scoped).As<IBoardViewFactory>();
            builder.Register<PieceViewFactory>(Lifetime.Scoped).As<IPieceViewFactory>();
            builder.Register<BoardPresenter>(Lifetime.Scoped).As<IBoardPresenter, IDisposable>();
            builder.RegisterInstance(_chessViewPrefabData);
        }
    }
}
