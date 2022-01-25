using System.Collections.Generic;
using Core.Managers.Logger;
using Core.MVC;

namespace Core.Managers.ViewManager
{
    public class ViewManager : IViewManager
    {
        private Dictionary<string, IController> _registeredViews =
            new Dictionary<string, IController>();

        public void RegisterView(string viewName, IController controller)
        {
            if (_registeredViews.ContainsKey(viewName) == false)
            {
                _registeredViews.Add(viewName, controller);
            }
        }

        public void ShowView(string viewName)
        {
            if (_registeredViews.ContainsKey(viewName) == true)
            {
                IController toOpen = _registeredViews[viewName];

                if (toOpen.ViewType == ViewType.Window)
                {
                    foreach (IController viewToClose in _registeredViews.Values)
                    {
                        viewToClose.SetShown(false);
                    }
                }

                toOpen.SetShown(true);
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
                IController toOpen = _registeredViews[viewName];
                if (toOpen.IsShown == false)
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

        public void Dispose()
        {
            foreach (IController controller in _registeredViews.Values)
            {
                controller.Dispose();
            }

            _registeredViews.Clear();
        }
    }
}