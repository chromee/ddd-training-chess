namespace Chess.Domain
{
    public enum PlayerColor { White, Black, }

    public class Player
    {
        public readonly PlayerColor Color;

        public Player(PlayerColor color)
        {
            Color = color;
        }

        public override string ToString() => $"{Color} Player";
    }

    public static class PlayerColorExtensions
    {
        public static PlayerColor Opponent(this PlayerColor color) =>
            color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
    }
}
