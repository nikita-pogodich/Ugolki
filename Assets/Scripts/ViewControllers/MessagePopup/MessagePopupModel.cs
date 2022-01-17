using Core.MVC;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupModel : ViewModel
    {
        private string _messageKey;
        public string MessageKey => _messageKey;

        public MessagePopupModel()
        { }

        public MessagePopupModel(string messageKey)
        {
            UpdateModel(messageKey);
        }

        public void UpdateModel(string messageKey)
        {
            _messageKey = messageKey;
        }
    }
}