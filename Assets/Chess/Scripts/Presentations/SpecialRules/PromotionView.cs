using System;
using Chess.Scripts.Applications.Pieces;
using Chess.Scripts.Applications.SpecialRules;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Scripts.Presentations.SpecialRules
{
    public class PromotionView : MonoBehaviour, IPromotionView
    {
        [SerializeField] private Button _knightButton;
        [SerializeField] private Button _bishopButton;
        [SerializeField] private Button _rookButton;
        [SerializeField] private Button _queenButton;

        private readonly Subject<PieceType> _onSelectPieceType = new();
        public IObservable<PieceType> OnSelectPieceType => _onSelectPieceType;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;

            _knightButton.onClick.AddListener(() => SelectType(PieceType.Knight));
            _bishopButton.onClick.AddListener(() => SelectType(PieceType.Bishop));
            _rookButton.onClick.AddListener(() => SelectType(PieceType.Rook));
            _queenButton.onClick.AddListener(() => SelectType(PieceType.Queen));
        }

        private void SelectType(PieceType type)
        {
            _onSelectPieceType.OnNext(type);
            _canvas.enabled = false;
        }

        public void ShowPromotionDialogue()
        {
            _canvas.enabled = true;
        }
    }
}
