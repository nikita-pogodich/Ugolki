using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.Managers.PoolingManager;
using Core.Managers.ViewManager;
using UgolkiController;
using UnityEngine;
using ViewControllers.MainMenu;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private MainMenuView _mainMenuView;

        [SerializeField]
        private PoolingManager.PoolingManager _poolingManagerRoot;

        private Logger.ILogger _logger;
        private IPoolingManager _poolingManager;
        private IViewManager _viewManager;
        private IUgolkiController _ugolkiController;
        private ILocalizationManager _localizationManager;

        private void Start()
        {
            _logger = new UnityLogger();
            LogManager.RegisterLogger(_logger);
            _poolingManager = _poolingManagerRoot;
            _localizationManager = new LocalizationManager.LocalizationManager();
            _viewManager = new ViewManager.ViewManager();
            _ugolkiController = new UgolkiController.UgolkiController();

            CreateMainMenu();
        }

        private void CreateMainMenu()
        {
            MainMenuViewController mainMenuViewController = new MainMenuViewController(
                _viewManager,
                _ugolkiController,
                _poolingManager,
                _localizationManager);

            mainMenuViewController.SetView(_mainMenuView);
        }
    }
}