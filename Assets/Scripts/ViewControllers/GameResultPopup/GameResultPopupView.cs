using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _gameResult;

        [SerializeField]
        private Button _backToMenu;

        [SerializeField]
        private Button _restartGame;

        [SerializeField]
        private FadeAnimationView _fadeAnimation;

        public event Action BackToMenu;
        public event Action RestartGame;

        public void SetGameResult(string gameResult)
        {
            _gameResult.text = gameResult;
        }

        public void FadeIn(Action onComplete)
        {
            _fadeAnimation.FadeIn(onComplete);
        }

        public void FadeOut(Action onComplete)
        {
            _fadeAnimation.FadeOut(onComplete);
        }

        private void Start()
        {
            _backToMenu.onClick.AddListener(OnBackToMenu);
            _restartGame.onClick.AddListener(OnRestartGame);
        }

        private void OnBackToMenu()
        {
            BackToMenu?.Invoke();
        }

        private void OnRestartGame()
        {
            RestartGame?.Invoke();
        }
    }
}