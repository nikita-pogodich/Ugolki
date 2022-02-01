using System;
using Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public class UgolkiRulesListItemView : BaseView, IUgolkiRulesListItemView
    {
        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private Button _selectButton;

        [SerializeField]
        private CanvasGroup _selectedBackground;

        public GameObject ItemView => gameObject;
        public event Action Selected;

        public void OnSelected()
        {
            Selected?.Invoke();
        }

        public void SetSelected(bool isSelected)
        {
            _selectedBackground.alpha = isSelected ? 1.0f : 0.0f;
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        private void Start()
        {
            _selectButton.onClick.AddListener(OnSelected);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(OnSelected);
        }
    }
}