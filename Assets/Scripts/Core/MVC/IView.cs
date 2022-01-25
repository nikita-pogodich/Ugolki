using Core.Managers.PoolingManager;

namespace Core.MVC
{
    public interface IView
    {
        void Release(string resourceKey, IPoolingManager poolingManager);
    }
}