using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements
{
    public abstract class Movement
    {
        public MoveAmount[] Movements { get; protected set; }
        protected IMovementCondition Condition;

        public bool CanExecute(Game game, Piece piece, Position dest) => Condition == null || Condition.CanExecute(game, piece, dest);
    }
}
