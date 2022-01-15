using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers.PoolingManager
{
    public class PoolingManager : IPoolingManager
    {
        private Dictionary<string, Stack<GameObject>> _pool = new Dictionary<string, Stack<GameObject>>();

        public void PrepareResource(string resourceKey, int poolSize)
        {
            if (_pool.ContainsKey(resourceKey) == false)
            {
                _pool.Add(resourceKey, new Stack<GameObject>());
            }

            for (int i = 0; i < poolSize; i++)
            {
                GameObject instance = InstantiateGameObject(resourceKey);
                _pool[resourceKey].Push(instance);
            }
        }

        public GameObject GetResource(string resourceKey)
        {
            GameObject result;

            if (_pool.ContainsKey(resourceKey) == true && _pool[resourceKey].Count > 0)
            {
                result = _pool[resourceKey].Pop();
            }
            else
            {
                result = InstantiateGameObject(resourceKey);
            }

            return result;
        }

        public void ReleaseResource(string resourceKey, GameObject resource)
        {
            if (_pool.ContainsKey(resourceKey) == true)
            {
                _pool[resourceKey].Push(resource);
            }
            else
            {
                Stack<GameObject> pool = new Stack<GameObject>();
                pool.Push(resource);
                _pool.Add(resourceKey, pool);
            }
        }

        private static GameObject InstantiateGameObject(string resourceKey)
        {
            return Object.Instantiate(Resources.Load(resourceKey, typeof(GameObject))) as GameObject;
        }
    }
}