using System;
using Core.MVC;

namespace ViewControllers.MessagePopup
{
    public interface IMessagePopupModel : IModel
    {
        string MessageKey { get; }
        event Action<string> MessageKeyChanged;
        void UpdateMessageKey(string messageKey);
    }
}