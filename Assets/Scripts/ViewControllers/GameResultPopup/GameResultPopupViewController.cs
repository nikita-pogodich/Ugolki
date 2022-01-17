using Core.Managers.LocalizationManager;
using Core.Managers.ViewManager;
using Core.MVC;
using Settings;
using Settings.LocalizationKeys;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupViewController : BaseViewController<GameResultPopupView, ViewModel>
    {
        private ILocalizationManager _localizationManager;
        private IViewManager _viewManager;
        private GameResultPopupModel _gameResultPopupModel;

        public override ViewType ViewType => ViewType.Popup;
        public override string Name => ViewNamesList.GameResultPopup;

        public GameResultPopupViewController(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        protected override void OnSetModel()
        {
            _gameResultPopupModel = this.Model as GameResultPopupModel;
            if (_gameResultPopupModel == null)
            {
                return;
            }

            string resultKey;
            if (_gameResultPopupModel.Player == Player.White)
            {
                resultKey = GameResultPopupLocalizationKeys.GamerResultWhiteWins;
            }
            else
            {
                resultKey = GameResultPopupLocalizationKeys.GamerResultBlackWins;
            }

            string resultText = _localizationManager.GetText(resultKey);
            this.View.SetGameResult(resultText);
        }

        protected override void OnViewAdded()
        {
            this.View.BackToMenu += OnBackToMenu;
            this.View.RestartGame += OnRestartGame;
        }

        protected override void OnRemoveModel()
        {
            this.View.BackToMenu -= OnBackToMenu;
            this.View.RestartGame -= OnRestartGame;
        }

        protected override void OnSetShown(bool isShown)
        {
            if (isShown == true)
            {
                this.View.FadeIn();
            }
            else
            {
                this.View.FadeOut();
            }
        }

        private void OnRestartGame()
        {
            _gameResultPopupModel.OnRestart();
            this.SetShown(false);
        }

        private void OnBackToMenu()
        {
            _gameResultPopupModel.OnBackToMenu();
            this.SetShown(false);
        }
    }
}