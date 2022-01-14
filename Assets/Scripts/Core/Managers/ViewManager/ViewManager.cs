using System.Collections.Generic;
using Core.Managers.Logger;
using Core.MVC;

namespace Core.Managers.ViewManager
{
    public class ViewManager : IViewManager
    {
        private ILogger _logger;
        private Dictionary<string, IViewController> _registeredViews = new Dictionary<string, IViewController>();

        public ViewManager(ILogger logger)
        {
            _logger = new UnityLogger();
        }

        public void RegisterView(string viewName, IViewController viewController)
        {
            if (_registeredViews.ContainsKey(viewName) == false)
            {
                _registeredViews.Add(viewName, viewController);
            }
        }

        public void ShowView(string viewName)
        {
            if (_registeredViews.ContainsKey(viewName) == true)
            {
                IViewController viewToOpen = _registeredViews[viewName];
                if (viewToOpen.IsShown == true)
                {
                    _logger.LogWarning($"View already shown: {viewName}");
                    return;
                }

                if (viewToOpen.ViewType == ViewType.Window)
                {
                    foreach (IViewController viewToClose in _registeredViews.Values)
                    {
                        viewToClose.SetShown(false);
                    }
                }

                viewToOpen.SetShown(true);
            }
            else
            {
                _logger.LogWarning($"View not registered: {viewName}");
            }
        }

        public void HideView(string viewName)
        {
            if (_registeredViews.ContainsKey(viewName) == true)
            {
                IViewController viewToOpen = _registeredViews[viewName];
                if (viewToOpen.IsShown == false)
                {
                    _logger.LogWarning($"View already hidden: {viewName}");
                    return;
                }

                _registeredViews[viewName].SetShown(false);
            }
            else
            {
                _logger.LogWarning($"View not registered: {viewName}");
            }
        }
    }
}