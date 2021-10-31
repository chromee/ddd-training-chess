using Chess.Scripts.Domains.Pieces;
using UniRx;

namespace Chess.Scripts.Applications.Pieces
{
    public class SelectedPieceRegistry
    {
        private readonly ReactiveProperty<Piece> _selectedPiece = new();
        internal IReadOnlyReactiveProperty<Piece> SelectedPiece => _selectedPiece;
        internal void Register(Piece piece) => _selectedPiece.Value = piece;
        internal void Unregister() => _selectedPiece.Value = null;
        internal bool ExistSelectedPiece => _selectedPiece.Value != null;
    }
}
