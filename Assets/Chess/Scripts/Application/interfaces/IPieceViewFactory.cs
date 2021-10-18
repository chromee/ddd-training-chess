using Chess.Domain.Pieces;

namespace Chess.Application.interfaces
{
    public interface IPieceViewFactory
    {
        void CreatePieceView(Piece piece);
    }
}
