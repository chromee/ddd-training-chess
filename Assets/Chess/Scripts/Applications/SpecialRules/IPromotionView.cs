using System;
using Chess.Scripts.Applications.Pieces;

namespace Chess.Scripts.Applications.SpecialRules
{
    public interface IPromotionView
    {
        IObservable<PieceType> OnSelectPieceType { get; }
        void ShowPromotionDialogue();
    }
}
