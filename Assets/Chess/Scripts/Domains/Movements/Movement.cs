using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public abstract class Movement
    {
        protected IMovementCondition Condition;
        public MoveAmount[] Movements { get; protected set; }

        public bool CanExecute(Game game, Piece piece, Position dest) => Condition == null || Condition.CanExecute(game, piece, dest);
    }
}
