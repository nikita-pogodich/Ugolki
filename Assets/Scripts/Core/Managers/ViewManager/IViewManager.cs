using Core.MVC;

namespace Core.Managers.ViewManager
{
    public interface IViewManager
    {
        
        void RegisterView(string viewName, IViewController viewController);
        void ShowView(string viewName);
        void HideView(string viewName);
    }
}