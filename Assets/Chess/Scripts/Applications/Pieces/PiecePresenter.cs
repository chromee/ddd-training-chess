using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Applications.Pieces
{
    public class PiecePresenter
    {
        private Piece _piece;
        private IPieceView _view;

        public void Bind(Piece piece, IPieceView view)
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
