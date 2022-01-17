using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.Managers.ViewManager;
using Settings;
using UgolkiController;
using UnityEngine;
using ViewControllers.MainMenu;
using ViewControllers.UgolkiGame;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PoolingManager.PoolingManager _poolingManager;

        [SerializeField]
        private UgolkiBoardView _ugolkiBoard;

        [SerializeField]
        private MainMenuView _mainMenuView;

        [SerializeField]
        private UgolkiGameView _ugolkiGameView;

        private const string _startView = ViewNamesList.MainMenu;

        private Logger.ILogger _logger;
        private IViewManager _viewManager;
        private IUgolkiController _ugolkiController;
        private ILocalizationManager _localizationManager;

        private void Start()
        {
            _logger = new UnityLogger();
            LogManager.RegisterLogger(_logger);
            _localizationManager = new LocalizationManager.LocalizationManager();
            _viewManager = new ViewManager.ViewManager();
            _ugolkiController = new UgolkiController.UgolkiController(_ugolkiBoard);
            _ugolkiBoard.Initialize(_poolingManager, _ugolkiController);

            RegisterViews();
            ShowStartView();
        }

        private void RegisterViews()
        {
            RegisterMainMenu();
            RegisterUgolkiGame();
        }

        private void RegisterMainMenu()
        {
            MainMenuViewController mainMenuViewController = new MainMenuViewController(
                _viewManager,
                _ugolkiController,
                _poolingManager,
                _localizationManager);

            mainMenuViewController.SetView(_mainMenuView);
            _viewManager.RegisterView(mainMenuViewController.Name, mainMenuViewController);
        }

        private void RegisterUgolkiGame()
        {
            UgolkiGameViewController ugolkiGameViewController = new UgolkiGameViewController(
                _viewManager,
                _ugolkiController,
                _localizationManager);

            ugolkiGameViewController.SetView(_ugolkiGameView);
            _viewManager.RegisterView(ugolkiGameViewController.Name, ugolkiGameViewController);
        }

        private void ShowStartView()
        {
            _viewManager.ShowView(_startView);
        }
    }
}