using Chess.Application.Dto;
using Chess.Domain.Pieces;

namespace Chess.Application.interfaces
{
    public interface IPieceViewFactory
    {
        IPieceView CreatePieceView(PieceData pieceData);
    }
}
