using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Applications.Pieces
{
    public class PiecePresenter
    {
        private readonly Piece _piece;
        private readonly IPieceView _view;

        public PiecePresenter(Piece piece, IPieceView view)
        {
            _piece = piece;
            _view = view;
        }

        public void UpdateView()
        {
            if (_piece.IsDead)
            {
                _view.Dead();
                return;
            }

            _view.SetPosition(_piece.Position.ToVector2());
        }
    }
}
