using Chess.Application.Dto;
using Chess.Application.interfaces;
using Chess.Domain.Boards;
using Chess.Domain.Pieces;

namespace Chess.Application.Services
{
    public class SelectedPieceRegistry
    {
        public IPieceView SelectedPiece { get; private set; }
        public void Register(IPieceView piece) => SelectedPiece = piece;
        public void Unregister() => SelectedPiece = null;
        internal Piece GetPiece(Board board) => board.GetPiece(SelectedPiece.Position.ToPosition());
    }
}
