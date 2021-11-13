using System;
using UniRx;

namespace Chess.Scripts.Applications.Games
{
    public interface IGameResultView
    {
        void ShowCheck();
        void ShowCheckmate();
        void ShowStalemate();
        void HideAll();
        IObservable<Unit> OnRestart { get; }
    }
}
