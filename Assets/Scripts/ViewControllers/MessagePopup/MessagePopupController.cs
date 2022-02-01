using Core.Managers.LocalizationManager;
using Core.Managers.Logger;
using Core.MVC;
using Settings;
using Settings.LocalizationKeys;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupController :
        BaseController<IMessagePopupView, IMessagePopupModel>,
        IMessagePopupController
    {
        private ILocalizationManager _localizationManager;

        public override ViewType ViewType => ViewType.Popup;
        public override string Name => ViewNamesList.MessagePopup;

        public MessagePopupController(
            IMessagePopupView view,
            IMessagePopupModel model,
            ILocalizationManager localizationManager) : base(view, model)
        {
            _localizationManager = localizationManager;
            OnMessageKeyChanged(this.Model.MessageKey);
            this.Model.MessageKeyChanged += OnMessageKeyChanged;
        }

        protected override void OnSetShown(bool isShown)
        {
            if (isShown == true)
            {
                this.View.FadeIn(OnFadeInComplete);
            }
        }

        protected override void OnDispose()
        {
            this.Model.MessageKeyChanged -= OnMessageKeyChanged;
        }

        private void OnMessageKeyChanged(string messageKey)
        {
            if (string.IsNullOrEmpty(messageKey) == true)
            {
                return;
            }

            bool hasMessage =
                MessagePopupLocalizationKeys.UgolkiMessagesMap.TryGetValue(messageKey, out string result);

            if (hasMessage == false)
            {
                LogManager.LogWarning($"Message not found: " + messageKey);
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