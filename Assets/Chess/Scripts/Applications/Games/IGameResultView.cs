using System;
using UniRx;

namespace Chess.Scripts.Applications.Games
{
    public interface IGameResultView
    {
        IObservable<Unit> OnRestart { get; }
        void ShowCheck();
        void ShowCheckmate();
        void ShowStalemate();
        void HideAll();
    }
}
