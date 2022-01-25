using System;
using Core.MVC;

namespace ViewControllers.GameResultPopup
{
    public interface IGameResultPopupView : IView
    {
        event Action BackToMenu;
        event Action RestartGame;
        void SetGameResult(string gameResult);
        void FadeIn(Action onComplete = null);
        void FadeOut(Action onComplete = null);
    }
}