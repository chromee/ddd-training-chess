using Chess.Scripts.Domains.Boards;
using Chess.Scripts.Domains.Movements.Conditions;
using Chess.Scripts.Domains.Pieces;

namespace Chess.Scripts.Domains.Movements.Moves
{
    public abstract class MoveBase
    {
        public Movement[] Movements { get; protected set; }
        protected IMoveConditions Conditions;
        public Piece KillPiece { get; protected set; }

        public bool CanExecute(Piece piece, Position dest, Board board) => Conditions == null || Conditions.CanExecute(piece, dest, board);
    }
}
