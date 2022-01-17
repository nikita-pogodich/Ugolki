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

        public event Action StartGame;

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
        }

        private void OnDestroy()
        {
            _startGame.onClick.RemoveListener(OnStartGame);
        }

        private void OnStartGame()
        {
            StartGame?.Invoke();
        }
    }
}