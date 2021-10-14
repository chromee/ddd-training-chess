using System.Collections.Generic;
using UnityEngine;

namespace Chess.UI
{
    public class BoardBehavior : MonoBehaviour
    {
        [SerializeField] private BoardSquare _boardSquarePrefab;
        [SerializeField] private float _offset;

        private readonly List<BoardSquare> _boardSquares = new List<BoardSquare>();

        public void BuildBoard(int colSize, int rowSize)
        {
            var size = _boardSquarePrefab.GetSize();
            var x = -(colSize / 2f * size.x + (colSize - 1) / 2f * _offset);
            var y = rowSize / 2f * size.y + (rowSize - 1) / 2f * _offset;

            for (var i = 0; i < colSize; i++)
            for (var j = 0; j < rowSize; j++)
            {
                var square = Instantiate(_boardSquarePrefab, new Vector3(x, y, 0.1f), Quaternion.identity);
                square.Initialize(new Vector2(i, j));
                _boardSquares.Add(square);
                x += size.x + _offset;
                y += size.y + _offset;
            }
        }
    }
}
