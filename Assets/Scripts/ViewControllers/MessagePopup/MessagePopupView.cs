using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ViewControllers.MessagePopup
{
    public class MessagePopupView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _messageText;

        [SerializeField]
        private CanvasGroup _messageVisible;

        [SerializeField]
        private float _fadeDuration;

        [SerializeField]
        private float _stayShownDuration;

        private Sequence _fadeAnimation;

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void FadeIn(Action onComplete)
        {
            Fade(endAlpha: 1.0f, onCompleteDelay: _stayShownDuration, Ease.InQuad, onComplete);
        }

        public void FadeOut(Action onComplete)
        {
            Fade(endAlpha: 0.0f, onCompleteDelay: 0.0f, Ease.OutQuad, onComplete);
        }

        private void Fade(float endAlpha, float onCompleteDelay, Ease ease, Action onComplete)
        {
            _fadeAnimation?.Kill();
            _fadeAnimation = DOTween.Sequence();

            Tween messageFade = _messageVisible.DOFade(endAlpha, _fadeDuration).SetEase(ease);
            _fadeAnimation
                .Append(messageFade)
                .Append(messageFade)
                .AppendInterval(onCompleteDelay);

            _fadeAnimation.OnComplete(() => onComplete?.Invoke());
        }
    }
}