using Core.Managers.LocalizationManager;
using Core.MVC;
using Settings;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupViewController : BaseViewController<GameResultPopupView, ViewModel>
    {
        private ILocalizationManager _localizationManager;

        public override ViewType ViewType => ViewType.Popup;
        public override string Name => ViewNamesList.GameResultPopup;

        public GameResultPopupViewController(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        protected override void OnSetModel()
        {
            base.OnSetModel();
        }

        protected override void OnSetShown(bool isShown)
        {
            base.OnSetShown(isShown);
        }
    }
}