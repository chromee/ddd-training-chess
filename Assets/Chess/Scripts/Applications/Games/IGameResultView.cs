namespace Chess.Scripts.Applications.Games
{
    public interface IGameResultView
    {
        void ShowCheck();
        void ShowCheckmate();
        void ShowStalemate();
        void HideAll();
    }
}
