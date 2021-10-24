using Chess.Domain;
using Chess.Domain.Boards;

namespace Chess.Application.interfaces
{
    public interface IBoardViewFactory
    {
        IBoardView CreateBoardView(Board board);
    }
}
