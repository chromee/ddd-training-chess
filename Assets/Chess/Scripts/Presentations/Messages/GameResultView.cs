using System;
using Chess.Scripts.Applications.Games;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Chess.Scripts.Presentations.Messages
{
    public class GameResultView : MonoBehaviour, IGameResultView
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            _panel.SetActive(false);
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
            _panel.SetActive(false);
        }

        private void ShowText(string message)
        {
            UniTask.Void(async () =>
            {
                _panel.SetActive(true);
                _text.text = message;
                await UniTask.Delay(2000);
                _panel.SetActive(false);
            });
        }
    }
}
