namespace Chess.Scripts.Applications.Pieces
{
    public interface IPieceViewFactory
    {
        IPieceView CreatePieceView(PieceDto pieceDto);
    }
}
