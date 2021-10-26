using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Application.interfaces;
using UniRx;
using UnityEngine;

namespace Chess.View.Views
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private BoardSquare _boardSquarePrefab;

        private readonly List<BoardSquare> _boardSquares = new();

        private readonly Subject<Vector2> _onClicked = new();
        public IObservable<Vector2> OnClicked => _onClicked;


        private void Awake()
        {
            for (var x = 0; x < 8; x++)
            for (var y = 0; y < 8; y++)
            {
                var square = Instantiate(_boardSquarePrefab, new Vector3(x, y, 0.1f), Quaternion.identity);
                square.Initialize(new Vector2(x, y));
                square.OnClicked += pos => _onClicked.OnNext(pos);
                _boardSquares.Add(square);
            }
        }

        private void OnDestroy()
        {
            _onClicked.Dispose();
        }

        public void SetMovable(Vector2 position)
        {
            _boardSquares.FirstOrDefault(v => v.Position == position)?.SetMovable();
        }

        public void ResetSquares()
        {
            foreach (var square in _boardSquares)
            {
                square.SetDefault();
            }
        }
    }
}
