using System;
using Chess.Scripts.Applications.Games;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Chess.Scripts.Presentations.Messages
{
    public class GameResultView : UiViewBase, IGameResultView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            Canvas.enabled = false;
            _restartButton.gameObject.SetActive(false);
        }

        public IObservable<Unit> OnRestart => _restartButton.OnClickAsObservable();

        public void ShowCheck()
        {
            ShowText("Check");
            _restartButton.gameObject.SetActive(false);
        }

        public void ShowCheckmate()
        {
            ShowTextEternal("Checkmate");
            _restartButton.gameObject.SetActive(true);
        }

        public void ShowDraw()
        {
            ShowTextEternal("Stalemate");
            _restartButton.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            Canvas.enabled = false;
        }

        private void ShowText(string message)
        {
            UniTask.Void(async () =>
            {
                Canvas.enabled = true;
                _text.text = message;
                await UniTask.Delay(2000);
                Canvas.enabled = false;
            });
        }

        private void ShowTextEternal(string message)
        {
            Canvas.enabled = true;
            _text.text = message;
        }
    }
}
