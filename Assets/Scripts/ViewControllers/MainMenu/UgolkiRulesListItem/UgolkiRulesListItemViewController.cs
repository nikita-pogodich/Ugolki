using System;
using Core.Managers.LocalizationManager;
using Core.MVC;
using Settings.LocalizationKeys;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public class UgolkiRulesListItemViewController : BaseViewController<UgolkiRulesListItemView, string>
    {
        private ILocalizationManager _localizationManager;
        private bool _isSelected;

        public event Action<UgolkiRulesListItemViewController> Selected;

        public UgolkiRulesListItemViewController(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;

            this.View.SetSelected(_isSelected);
        }

        protected override void OnSetModel()
        {
            UpdateController();
        }

        protected override void OnViewAdded()
        {
            UpdateController();

            this.View.Selected += OnSelected;
        }

        protected override void OnViewRemoved()
        {
            this.View.Selected -= OnSelected;
        }

        private void UpdateController()
        {
            if (this.HasModel == false || this.HasView == false)
            {
                return;
            }

            SetTitle();
        }

        private void SetTitle()
        {
            bool ruleHasLocalizationKey =
                MainMenuLocalizationKeys.UgolkiRulesMap.TryGetValue(this.Model, out string titleKey);
            if (ruleHasLocalizationKey == false)
            {
                return;
            }

            string title = _localizationManager.GetText(titleKey);
            this.View.SetTitle(title);
        }

        private void OnSelected()
        {
            Selected?.Invoke(this);
        }
    }
}