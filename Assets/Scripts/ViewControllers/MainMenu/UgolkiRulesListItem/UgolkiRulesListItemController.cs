using System;
using Core.Managers.LocalizationManager;
using Core.MVC;
using Settings.LocalizationKeys;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public class UgolkiRulesListItemController :
        BaseController<IUgolkiRulesListItemView, IUgolkiRulesListItemModel>,
        IUgolkiRulesListItemController
    {
        private ILocalizationManager _localizationManager;
        private bool _isSelected;

        public event Action<IUgolkiRulesListItemController> Selected;

        public UgolkiRulesListItemController(
            IUgolkiRulesListItemView view,
            IUgolkiRulesListItemModel model,
            ILocalizationManager localizationManager)
            : base(view, model)
        {
            _localizationManager = localizationManager;
            view.Selected += OnSelected;
            model.RuleKeyChanged += OnSetRuleKeyChanged;

            OnSetRuleKeyChanged(model.RuleKey);
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;

            this.View.SetSelected(_isSelected);
        }

        public void OnLocalizationChanged()
        {
            OnSetRuleKeyChanged(this.Model.RuleKey);
        }

        protected override void OnDispose()
        {
            this.View.Selected -= OnSelected;
            this.Model.RuleKeyChanged += OnSetRuleKeyChanged;
        }

        private void OnSetRuleKeyChanged(string ruleKey)
        {
            bool ruleHasLocalizationKey =
                MainMenuLocalizationKeys.UgolkiRulesMap.TryGetValue(ruleKey, out string titleKey);
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