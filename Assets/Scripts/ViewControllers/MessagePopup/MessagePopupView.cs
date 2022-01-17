using System;
using TMPro;
using Tools;
using UnityEngine;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _messageText;

        [SerializeField]
        private FadeAnimationView _fadeAnimation;

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void FadeIn(Action onComplete)
        {
            _fadeAnimation.FadeIn(onComplete);
        }

        public void FadeOut(Action onComplete)
        {
            _fadeAnimation.FadeOut(onComplete);
        }
    }
}