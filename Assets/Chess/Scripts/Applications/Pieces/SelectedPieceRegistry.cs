using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Applications.Pieces
{
    public class SelectedPieceRegistry
    {
        internal Piece SelectedPiece { get; private set; }
        internal void Register(Piece piece) => SelectedPiece = piece;
        internal void Unregister() => SelectedPiece = null;
        internal bool ExistSelectedPiece => SelectedPiece != null;
    }
}
