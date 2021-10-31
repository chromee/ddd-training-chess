namespace Chess.Scripts.Applications.Games
{
    public class GameRegistry
    {
        public Domains.Games.Game CurrentGame { get; private set; }
        public void Register(Domains.Games.Game game) => CurrentGame = game;
    }
}
