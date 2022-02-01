using System;
using Core.MVC;

namespace Core.Managers.ViewManager
{
    public interface IViewManager : IDisposable
    {
        void RegisterView(string viewName, IController controller);
        void ShowView(string viewName);
        void HideView(string viewName);
    }
}