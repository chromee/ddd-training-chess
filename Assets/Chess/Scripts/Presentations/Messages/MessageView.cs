using System;
using System.Threading;
using Chess.Scripts.Applications.Messages;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Chess.Scripts.Presentations.Messages
{
    public class MessageView : UiViewBase, IMessagePublisher
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;

        private CancellationTokenSource _cts;

        private void Start()
        {
            Canvas.enabled = false;

            _text.text = string.Empty;
        }

        public void ShowMessage(string message)
        {
            UniTask.Void(async () =>
            {
                if (_cts != null)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                }

                _text.text = message;
                _text.alpha = 1;
                Canvas.enabled = true;

                _cts = new CancellationTokenSource();
                await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: _cts.Token);

                _text.text = string.Empty;
                _text.alpha = 0;
                Canvas.enabled = false;
            });
        }
    }
}
