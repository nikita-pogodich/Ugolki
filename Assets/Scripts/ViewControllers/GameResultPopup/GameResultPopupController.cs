using Core.Managers.LocalizationManager;
using Core.Managers.ViewManager;
using Core.MVC;
using Settings;
using Settings.LocalizationKeys;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupController :
        BaseController<IGameResultPopupView, IGameResultPopupModel>,
        IGameResultPopupController
    {
        private ILocalizationManager _localizationManager;
        private IViewManager _viewManager;

        public override ViewType ViewType => ViewType.Popup;
        public override string Name => ViewNamesList.GameResultPopup;

        public GameResultPopupController(
            IGameResultPopupView view,
            IGameResultPopupModel model,
            ILocalizationManager localizationManager) : base(view, model)
        {
            _localizationManager = localizationManager;
            OnPlayerInfoInfoChanged(this.Model.Player);
            model.PlayerInfoChanged += OnPlayerInfoInfoChanged;
            view.BackToMenu += OnBackToMenu;
            view.RestartGame += OnRestartGame;
        }

        protected override void OnDispose()
        {
            this.View.BackToMenu -= OnBackToMenu;
            this.View.RestartGame -= OnRestartGame;
            this.Model.PlayerInfoChanged -= OnPlayerInfoInfoChanged;
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

        private void OnPlayerInfoInfoChanged(Player player)
        {
            string resultKey;
            if (player == Player.White)
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

        private void OnRestartGame()
        {
            this.Model.OnRestart();
            this.SetShown(false);
        }

        private void OnBackToMenu()
        {
            this.Model.OnBackToMenu();
            this.SetShown(false);
        }
    }
}