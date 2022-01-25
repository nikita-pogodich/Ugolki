using Core.Managers.PoolingManager;
using UnityEngine;

namespace Core.MVC
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        void IView.Release(string resourceKey, IPoolingManager poolingManager)
        {
            poolingManager.ReleaseResource(resourceKey, gameObject);
        }
    }
}