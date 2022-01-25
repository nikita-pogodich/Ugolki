using System;
using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.Managers.ViewManager;
using Core.MVC;
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
        private IViewFactory _viewFactory;

        private IMessagePopupModel _messagePopupModel = new MessagePopupModel();
        private IGameResultPopupModel _gameResultPopupModel = new GameResultPopupModel();

        private void Start()
        {
            _logger = new UnityLogger();
            LogManager.RegisterLogger(_logger);
            _localizationManager = new StubLocalizationManager();
            _viewManager = new ViewManager.ViewManager();
            _viewFactory = new ViewFactory(_poolingManager);
            _ugolkiController = new UgolkiController.UgolkiController(_ugolkiBoard);

            _ugolkiBoard.Initialize(
                _poolingManager,
                _ugolkiController,
                _viewManager,
                _messagePopupModel,
                _gameResultPopupModel);

            RegisterViews();
            ShowStartView();
        }

        private void OnDestroy()
        {
            _viewManager.Dispose();
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
            IMainMenuController mainMenuController = new MainMenuController(
                _viewManager,
                _ugolkiController,
                _viewFactory,
                _localizationManager,
                _mainMenuView);

            _viewManager.RegisterView(mainMenuController.Name, mainMenuController);
        }

        private void RegisterUgolkiGame()
        {
            IUgolkiGameController ugolkiGameViewController = new UgolkiGameController(
                _viewManager,
                _ugolkiController,
                _localizationManager,
                _ugolkiGameView);

            _viewManager.RegisterView(ugolkiGameViewController.Name, ugolkiGameViewController);
        }

        private void RegisterMessagePopup()
        {
            IMessagePopupController messagePopupViewController =
                new MessagePopupController(_messagePopupView, _messagePopupModel, _localizationManager);

            _viewManager.RegisterView(messagePopupViewController.Name, messagePopupViewController);
        }

        private void RegisterGameResultPopup()
        {
            IGameResultPopupController gameResultPopupController =
                new GameResultPopupController(_gameResultPopupView, _gameResultPopupModel, _localizationManager);

            _viewManager.RegisterView(gameResultPopupController.Name, gameResultPopupController);
        }
    }
}