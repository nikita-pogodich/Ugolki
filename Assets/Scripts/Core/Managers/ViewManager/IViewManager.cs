using Core.MVC;

namespace Core.Managers.ViewManager
{
    public interface IViewManager
    {
        void RegisterView(string viewName, IViewController<ViewModel> viewController);
        void ShowView(string viewName, ViewModel model = null);
        void HideView(string viewName);
    }
}