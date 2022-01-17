using System.Collections.Generic;
using Core.Managers.Logger;
using Core.MVC;

namespace Core.Managers.ViewManager
{
    public class ViewManager : IViewManager
    {
        private Dictionary<string, IViewController<ViewModel>> _registeredViews =
            new Dictionary<string, IViewController<ViewModel>>();

        public void RegisterView(string viewName, IViewController<ViewModel> viewController)
        {
            if (_registeredViews.ContainsKey(viewName) == false)
            {
                _registeredViews.Add(viewName, viewController);
            }
        }

        public void ShowView(string viewName, ViewModel model)
        {
            if (_registeredViews.ContainsKey(viewName) == true)
            {
                IViewController<ViewModel> viewToOpen = _registeredViews[viewName];

                if (viewToOpen.ViewType == ViewType.Window)
                {
                    foreach (IViewController<ViewModel> viewToClose in _registeredViews.Values)
                    {
                        viewToClose.SetShown(false);
                    }
                }

                if (model != null)
                {
                    viewToOpen.SetModel(model);
                }

                viewToOpen.SetShown(true);
            }
            else
            {
                LogManager.LogWarning($"View not registered: {viewName}");
            }
        }

        public void HideView(string viewName)
        {
            if (_registeredViews.ContainsKey(viewName) == true)
            {
                IViewController<ViewModel> viewToOpen = _registeredViews[viewName];
                if (viewToOpen.IsShown == false)
                {
                    LogManager.LogWarning($"View already hidden: {viewName}");
                    return;
                }

                _registeredViews[viewName].SetShown(false);
            }
            else
            {
                LogManager.LogWarning($"View not registered: {viewName}");
            }
        }
    }
}