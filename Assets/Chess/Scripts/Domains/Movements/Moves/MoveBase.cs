using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public abstract class MoveBase
    {
        public Movement[] Movements { get; protected set; }
        protected IMoveCondition Condition;

        public bool CanExecute(Game game, Piece piece, Position dest) => Condition == null || Condition.CanExecute(game, piece, dest);
    }
}
