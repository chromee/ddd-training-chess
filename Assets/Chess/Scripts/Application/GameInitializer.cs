using System;
using Chess.Domain;
using Chess.Domain.Movements;
using Chess.Domain.Pieces;
using Chess.View;
using TMPro;
using UnityEngine;

namespace Chess.Application
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private BoardBehavior _boardPrefab;
        [SerializeField] private PieceBehavior _piecePrefab;

        [SerializeField] private CheckmateText _checkmateText;

        public Game Game { get; private set; }

        private Piece _selectedPiece;

        private void Start()
        {
            var gameFactory = new GameFactory();

            // TODO: Serviceの乱立をなんとかする
            var pieceService = new PieceService();
            var checkService = new CheckService(pieceService);
            var moveService = new MoveService(pieceService, checkService);
            var checkmateService = new CheckmateService(pieceService, moveService, checkService);

            Game = gameFactory.CreateGame(moveService);

            var board = Instantiate(_boardPrefab);

            foreach (var piece in Game.Board.Pieces)
            {
                var pieceBehavior = Instantiate(_piecePrefab);
                pieceBehavior.Initialize(piece);
            }

            foreach (var square in board.Squares)
            {
                square.OnClicked += pos =>
                {
                    if (_selectedPiece != null && square.IsMovable)
                    {
                        try
                        {
                            Game.MovePiece(_selectedPiece, square.Position);
                            board.ResetSquares();
                            if (checkmateService.IsCheckmate(Game.Board, Game.NextTurnPlayerColor))
                            {
                                _checkmateText.Show(Game.NextTurnPlayerColor);
                            }
                        }
                        catch (ArgumentException e)
                        {
                            Debug.LogError(e);
                        }
                    }

                    var piece = Game.Board.GetPiece(pos);
                    if (!square.IsMovable && piece.IsOwner(Game.CurrentTurnPlayer))
                    {
                        _selectedPiece = piece;
                        board.ResetSquares();
                        foreach (var move in pieceService.MoveCandidates(piece, Game.Board))
                        {
                            board.SetMovable(move);
                        }
                    }
                };
            }
        }
    }
}
