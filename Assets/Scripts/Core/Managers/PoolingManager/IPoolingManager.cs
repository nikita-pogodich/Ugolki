using UnityEngine;

namespace Core.Managers.PoolingManager
{
    public interface IPoolingManager
    {
        void PrepareResource(string resourceKey, int poolSize);
        GameObject GetResource(string resourceKey);
        void ReleaseResource(string resourceKey, GameObject resource);
    }
}