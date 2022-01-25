using Core.Managers.PoolingManager;
using UnityEngine;

namespace Core.MVC
{
    public class ViewFactory : IViewFactory
    {
        private IPoolingManager _poolingManager;

        public ViewFactory(IPoolingManager poolingManager)
        {
            _poolingManager = poolingManager;
        }

        public TView Create<TView>(string resourceKey) where TView : IView
        {
            GameObject resource = _poolingManager.GetResource(resourceKey);
            TView view = resource.GetComponent<TView>();
            return view;
        }

        public void Destroy<TView>(string resourceKey, TView view) where TView : IView
        {
            view.Release(resourceKey, _poolingManager);
        }
    }
}