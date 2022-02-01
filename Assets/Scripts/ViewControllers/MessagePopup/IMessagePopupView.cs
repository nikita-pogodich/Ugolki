using System;
using Core.MVC;

namespace ViewControllers.MessagePopup
{
    public interface IMessagePopupView : IView
    {
        void SetMessage(string message);
        void FadeIn(Action onComplete);
        void FadeOut(Action onComplete);
    }
}