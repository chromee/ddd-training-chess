using Chess.Domain.Pieces;

namespace Chess.Application.Services
{
    public class SelectedPieceRegistry
    {
        public Piece SelectedPiece { get; private set; }
        public void Register(Piece piece) => SelectedPiece = piece;
        public void Unregister() => SelectedPiece = null;
    }
}
