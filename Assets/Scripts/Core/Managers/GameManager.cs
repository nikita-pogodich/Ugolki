using Core.Managers.Logger;
using Core.Managers.PoolingManager;
using Core.Managers.ViewManager;
using UnityEngine;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        private Logger.ILogger _logger;
        private IPoolingManager _poolingManager;
        private IViewManager _viewManager;

        private void Start()
        {
            _logger = new UnityLogger();
            _poolingManager = new PoolingManager.PoolingManager();
            _viewManager = new ViewManager.ViewManager(_logger);
        }
    }
}