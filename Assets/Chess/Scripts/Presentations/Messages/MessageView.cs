using System;
using System.Threading;
using Chess.Scripts.Applications.Messages;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Chess.Scripts.Presentations.Messages
{
    public class MessageView : MonoBehaviour, IMessagePublisher
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;

        private CancellationTokenSource _cts;

        private void Start()
        {
            _text.text = string.Empty;
            _text.enabled = false;
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
                _text.enabled = true;

                _cts = new CancellationTokenSource();
                await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: _cts.Token);

                _text.text = string.Empty;
                _text.alpha = 0;
                _text.enabled = false;
            });
        }
    }
}
