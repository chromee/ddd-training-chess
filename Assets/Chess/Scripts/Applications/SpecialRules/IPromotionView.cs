using System;
using Chess.Scripts.Applications.Pieces;

namespace Chess.Scripts.Applications.SpecialRules
{
    public interface IPromotionView
    {
        void ShowPromotionDialogue();
        IObservable<PieceType> OnSelectPieceType { get; }
    }
}
