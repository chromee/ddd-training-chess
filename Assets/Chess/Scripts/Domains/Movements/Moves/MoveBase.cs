using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Games;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public abstract class MoveBase
    {
        public Movement[] Movements { get; protected set; }
        protected IMoveConditions Conditions;

        public bool CanExecute(Game game, Piece piece, Position dest) => Conditions == null || Conditions.CanExecute(game, piece, dest);
    }
}
