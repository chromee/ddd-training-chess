namespace Chess.Scripts.Applications.Pieces
{
    public interface IPieceViewFactory
    {
        IPieceView CreatePieceView(PieceData pieceData);
    }
}
