using System;
using Chess.Application.Dto;
using Chess.Application.interfaces;
using Chess.Domain;
using Chess.Domain.Games;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;
using UnityEngine;
using VContainer.Unity;

namespace Chess.Application.Services
{
    public class GameInitializer : IInitializable
    {
        private readonly IGameFactory _gameFactory;

        private readonly GameRegistry _gameRegistry;
        private readonly PiecesRegistry _piecesRegistry;
        private readonly IBoardViewFactory _boardViewFactory;
        private readonly IPieceViewFactory _pieceViewFactory;
        private readonly IBoardPresenter _boardPresenter;

        public GameInitializer(IGameFactory gameFactory, GameRegistry gameRegistry, IBoardViewFactory boardViewFactory,
            IPieceViewFactory pieceViewFactory, IBoardPresenter boardPresenter)
        {
            _gameFactory = gameFactory;
            _gameRegistry = gameRegistry;
            _boardViewFactory = boardViewFactory;
            _pieceViewFactory = pieceViewFactory;
            _boardPresenter = boardPresenter;
        }


        public void Initialize()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            var game = _gameFactory.CreateGame();
            var boardView = _boardViewFactory.CreateBoardView();
            foreach (var piece in game.Board.Pieces)
            {
                _piecesRegistry.AddPiece(_pieceViewFactory.CreatePieceView(piece.ToData()));
            }

            _gameRegistry.Register(game);
            _boardPresenter.Bind(boardView);
        }
    }
}
