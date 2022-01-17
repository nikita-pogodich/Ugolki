using System.Collections.Generic;
using Core.Managers.Logger;
using UnityEngine;

namespace Core.Managers.PoolingManager
{
    public class PoolingManager : MonoBehaviour, IPoolingManager
    {
        [SerializeField]
        private Transform _poolRoot;

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

            resource.transform.SetParent(_poolRoot);
        }

        private GameObject InstantiateGameObject(string resourceKey)
        {
            Object resource = Resources.Load(resourceKey, typeof(GameObject));
            if (resource == null)
            {
                LogManager.LogError($"Resource not found: {resourceKey}");
                return null;
            }

            GameObject result = Instantiate(
                original: resource,
                parent: _poolRoot,
                instantiateInWorldSpace: true) as GameObject;

            if (result != null)
            {
                result.name = resource.name;
            }

            return result;
        }
    }
}