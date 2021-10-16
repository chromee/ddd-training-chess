using System;
using Chess.Domain;
using Chess.View;
using UnityEngine;

namespace Chess.Application
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private PieceBehavior _piecePrefab;

        public Game Game { get; private set; }

        private void Start()
        {
            var gameFactory = new GameFactory();
            Game = gameFactory.CreateGame();
            foreach (var piece in Game.Board.Pieces)
            {
                var pieceBehavior = Instantiate(_piecePrefab);
                pieceBehavior.Initialize(piece, Game.Board);
            }
        }
    }
}
