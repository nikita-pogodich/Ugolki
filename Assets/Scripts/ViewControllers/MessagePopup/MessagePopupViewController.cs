using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.MVC;
using Settings;
using Settings.LocalizationKeys;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupViewController : BaseViewController<MessagePopupView, ViewModel>
    {
        private ILocalizationManager _localizationManager;

        public override ViewType ViewType => ViewType.Popup;
        public override string Name => ViewNamesList.MessagePopup;

        public MessagePopupViewController(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        protected override void OnSetShown(bool isShown)
        {
            if (isShown == true)
            {
                this.View.FadeIn(OnFadeInComplete);
            }
        }

        protected override void OnSetModel()
        {
            MessagePopupModel model = this.Model as MessagePopupModel;
            if (model == null)
            {
                return;
            }

            bool hasMessage =
                MessagePopupLocalizationKeys.UgolkiMessagesMap.TryGetValue(model.MessageKey, out string result);

            if (hasMessage == false)
            {
                LogManager.LogWarning($"Message not found: " + model.MessageKey);
                return;
            }

            string messageText = _localizationManager.GetText(result);
            this.View.SetMessage(messageText);
        }

        private void OnFadeInComplete()
        {
            this.View.FadeOut(OnFadeOutComplete);
        }

        private void OnFadeOutComplete()
        {
            this.SetShown(false);
        }
    }
}