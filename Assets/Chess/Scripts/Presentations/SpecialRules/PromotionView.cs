using System;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Applications.SpecialRules;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Scripts.Presentations.SpecialRules
{
    public class PromotionView : UiViewBase, IPromotionView
    {
        [SerializeField] private Button _knightButton;
        [SerializeField] private Button _bishopButton;
        [SerializeField] private Button _rookButton;
        [SerializeField] private Button _queenButton;

        private readonly Subject<PieceType> _onSelectPieceType = new();

        private void Awake()
        {
            Canvas.enabled = false;

            _knightButton.onClick.AddListener(() => SelectType(PieceType.Knight));
            _bishopButton.onClick.AddListener(() => SelectType(PieceType.Bishop));
            _rookButton.onClick.AddListener(() => SelectType(PieceType.Rook));
            _queenButton.onClick.AddListener(() => SelectType(PieceType.Queen));
        }

        public IObservable<PieceType> OnSelectPieceType => _onSelectPieceType;

        public void ShowPromotionDialogue()
        {
            Canvas.enabled = true;
        }

        private void SelectType(PieceType type)
        {
            _onSelectPieceType.OnNext(type);
            Canvas.enabled = false;
        }
    }
}
