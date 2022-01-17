using System;
using DG.Tweening;
using UnityEngine;

namespace Tools
{
    public class FadeAnimationView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _visible;

        [SerializeField]
        private float _fadeDuration;

        [SerializeField]
        private float _stayShownDuration;

        private Sequence _fadeAnimation;

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

            Tween fadeTween = _visible.DOFade(endAlpha, _fadeDuration).SetEase(ease);
            _fadeAnimation
                .Append(fadeTween)
                .Append(fadeTween)
                .AppendInterval(onCompleteDelay);

            _fadeAnimation.OnComplete(() => onComplete?.Invoke());
        }
    }
}