using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public abstract class Movement
    {
        public MoveAmount[] Movements { get; protected set; }

        public abstract bool CanExecute(Game game, Piece piece, Position destination);
    }
}
