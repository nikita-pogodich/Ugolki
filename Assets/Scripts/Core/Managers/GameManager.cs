using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.Managers.ViewManager;
using Settings;
using UgolkiController;
using UnityEngine;
using ViewControllers.GameResultPopup;
using ViewControllers.MainMenu;
using ViewControllers.MessagePopup;
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

        [SerializeField]
        private MessagePopupView _messagePopupView;

        [SerializeField]
        private GameResultPopupView _gameResultPopupView;

        private const string _startView = ViewNamesList.MainMenu;

        private Logger.ILogger _logger;
        private IViewManager _viewManager;
        private IUgolkiController _ugolkiController;
        private ILocalizationManager _localizationManager;

        private void Start()
        {
            _logger = new UnityLogger();
            LogManager.RegisterLogger(_logger);
            _localizationManager = new LocalizationManager.StubLocalizationManager();
            _viewManager = new ViewManager.ViewManager();
            _ugolkiController = new UgolkiController.UgolkiController(_ugolkiBoard);
            _ugolkiBoard.Initialize(_poolingManager, _ugolkiController, _viewManager);

            RegisterViews();
            ShowStartView();
        }

        private void RegisterViews()
        {
            RegisterMainMenu();
            RegisterUgolkiGame();
            RegisterMessagePopup();
            RegisterGameResultPopup();
        }

        private void ShowStartView()
        {
            _viewManager.ShowView(_startView);
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

        private void RegisterMessagePopup()
        {
            MessagePopupViewController messagePopupViewController =
                new MessagePopupViewController(_localizationManager);
            messagePopupViewController.SetView(_messagePopupView);

            _viewManager.RegisterView(messagePopupViewController.Name, messagePopupViewController);
        }

        private void RegisterGameResultPopup()
        {
            GameResultPopupViewController gameResultPopupViewController =
                new GameResultPopupViewController(_localizationManager);

            gameResultPopupViewController.SetView(_gameResultPopupView);
            _viewManager.RegisterView(gameResultPopupViewController.Name, gameResultPopupViewController);
        }
    }
}