using System;
using UnityEngine;
using UnityEngine.UI;
using ViewControllers.MainMenu.UgolkiRulesList;

namespace ViewControllers.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private UgolkiRulesListView _ugolkiRulesList;

        [SerializeField]
        private CanvasGroup _visible;

        [SerializeField]
        private Button _startGame;

        [SerializeField]
        private Button _exit;

        public event Action StartGame;
        public event Action ExitGame;

        public UgolkiRulesListView UgolkiRulesList => _ugolkiRulesList;

        public void SetShown(bool isShown)
        {
            _visible.alpha = isShown ? 1.0f : 0.0f;
            _visible.interactable = isShown;
            _visible.blocksRaycasts = isShown;
        }

        private void Start()
        {
            _startGame.onClick.AddListener(OnStartGame);
            _exit.onClick.AddListener(OnExitGame);
        }

        private void OnDestroy()
        {
            _startGame.onClick.RemoveListener(OnStartGame);
            _exit.onClick.RemoveListener(OnExitGame);
        }

        private void OnStartGame()
        {
            StartGame?.Invoke();
        }

        private void OnExitGame()
        {
            ExitGame?.Invoke();
        }
    }
}