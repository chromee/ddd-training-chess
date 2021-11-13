using System;
using Chess.Scripts.Applications.Games;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Scripts.Presentations.Messages
{
    public class GameResultView : MonoBehaviour, IGameResultView
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public void ShowCheck()
        {
            ShowText("Check");
        }

        public void ShowCheckmate()
        {
            ShowText("Checkmate");
        }

        public void ShowStalemate()
        {
            ShowText("Stalemate");
        }

        public void HideAll()
        {
            _canvas.enabled = false;
        }

        private void ShowText(string message)
        {
            UniTask.Void(async () =>
            {
                _canvas.enabled = true;
                _text.text = message;
                await UniTask.Delay(2000);
                _canvas.enabled = false;
            });
        }
    }
}
