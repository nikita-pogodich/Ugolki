using System;
using Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewControllers.UgolkiGame
{
    public class UgolkiGameView : BaseView, IUgolkiGameView
    {
        [SerializeField]
        private Button _back;

        [SerializeField]
        private TextMeshProUGUI _whiteMovesCount;

        [SerializeField]
        private TextMeshProUGUI _blackMovesCount;

        [SerializeField]
        private TextMeshProUGUI _currentPlayer;

        [SerializeField]
        private CanvasGroup _visible;

        public event Action Back;

        private void Start()
        {
            _back.onClick.AddListener(OnBack);
        }

        private void OnDestroy()
        {
            _back.onClick.RemoveListener(OnBack);
        }

        public void SetWhiteMovesCount(string count)
        {
            _whiteMovesCount.text = count;
        }

        public void SetBlackMovesCount(string count)
        {
            _blackMovesCount.text = count;
        }

        public void ChangeCurrentPlayer(string currentPlayer)
        {
            _currentPlayer.text = currentPlayer;
        }

        public void SetShown(bool isShown)
        {
            _visible.alpha = isShown ? 1.0f : 0.0f;
            _visible.interactable = isShown;
            _visible.blocksRaycasts = isShown;
        }

        private void OnBack()
        {
            Back?.Invoke();
        }
    }
}