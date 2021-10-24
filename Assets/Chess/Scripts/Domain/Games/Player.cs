namespace Chess.Domain.Games
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
}
