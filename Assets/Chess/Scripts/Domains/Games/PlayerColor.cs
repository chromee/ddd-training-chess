namespace Chess.Scripts.Domains.Games
{
    public enum PlayerColor
    {
        White,
        Black,
    }

    public static class PlayerColorExtensions
    {
        public static PlayerColor Opponent(this PlayerColor color) => color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
    }
}
