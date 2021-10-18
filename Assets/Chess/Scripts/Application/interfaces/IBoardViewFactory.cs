using Chess.Domain;

namespace Chess.Application.interfaces
{
    public interface IBoardViewFactory
    {
        IBoardView CreateBoardView(Board board);
    }
}
