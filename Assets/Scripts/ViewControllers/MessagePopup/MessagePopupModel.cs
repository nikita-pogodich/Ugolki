using System;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupModel : IMessagePopupModel
    {
        public string MessageKey { get; }

        public event Action<string> MessageKeyChanged;

        public MessagePopupModel()
        { }

        public MessagePopupModel(string messageKey)
        {
            MessageKey = messageKey;
        }

        public void UpdateMessageKey(string messageKey)
        {
            MessageKeyChanged?.Invoke(messageKey);
        }
    }
}