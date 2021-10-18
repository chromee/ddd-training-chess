using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain;
using UnityEngine;

namespace Chess.View
{
    public class BoardBehavior : MonoBehaviour
    {
        [SerializeField] private BoardSquare _boardSquarePrefab;

        private readonly List<BoardSquare> _boardSquares = new();
        public IReadOnlyList<BoardSquare> Squares => _boardSquares;

        private void Awake()
        {
            for (var x = 0; x < 8; x++)
            for (var y = 0; y < 8; y++)
            {
                var square = Instantiate(_boardSquarePrefab, new Vector3(x, y, 0.1f), Quaternion.identity);
                square.Initialize(new Position(x, y));
                _boardSquares.Add(square);
            }
        }

        public void SetMovable(Position position)
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
