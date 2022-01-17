using System;
using System.Collections.Generic;
using Core.Managers.LocalizationManager;
using Core.Managers.PoolingManager;
using Core.MVC;
using Settings;
using UnityEngine;
using ViewControllers.MainMenu.UgolkiRulesListItem;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public class UgolkiRulesListViewController : BaseViewController<UgolkiRulesListView, List<string>>
    {
        private IPoolingManager _poolingManager;
        private ILocalizationManager _localizationManager;

        private const int _defaultSelectedRule = 0;

        private List<UgolkiRulesListItemViewController> _ugolkiRulesList =
            new List<UgolkiRulesListItemViewController>();

        public event Action<string> RuleSelected;

        public UgolkiRulesListViewController(IPoolingManager poolingManager, ILocalizationManager localizationManager)
        {
            _poolingManager = poolingManager;
            _localizationManager = localizationManager;

            _localizationManager.LocalizationChanged += OnLocalizationChanged;
        }

        protected override void OnSetModel()
        {
            UpdateController();
        }

        protected override void OnViewAdded()
        {
            UpdateController();
        }

        private void UpdateController()
        {
            if (this.HasModel == false || this.HasView == false)
            {
                return;
            }

            for (int i = 0; i < this.Model.Count; i++)
            {
                GameObject ruleListObject = _poolingManager.GetResource(ResourceNamesList.UgolkiRulesListItem);
                UgolkiRulesListItemView rulesListItemView = ruleListObject.GetComponent<UgolkiRulesListItemView>();
                UgolkiRulesListItemViewController rulesListItem =
                    new UgolkiRulesListItemViewController(_localizationManager);
                rulesListItem.SetModel(this.Model[i]);
                rulesListItem.SetView(rulesListItemView);
                rulesListItem.Selected += SelectRule;
                _ugolkiRulesList.Add(rulesListItem);

                this.View.AddItem(rulesListItemView.gameObject);
            }

            SelectRule(_ugolkiRulesList[_defaultSelectedRule]);
        }

        protected override void OnDispose()
        {
            for (int i = 0; i < _ugolkiRulesList.Count; i++)
            {
                _ugolkiRulesList[i].Selected -= SelectRule;
            }

            _localizationManager.LocalizationChanged -= OnLocalizationChanged;
        }

        private void SelectRule(UgolkiRulesListItemViewController ugolkiRulesListItem)
        {
            for (int i = 0; i < _ugolkiRulesList.Count; i++)
            {
                if (_ugolkiRulesList[i] == ugolkiRulesListItem)
                {
                    continue;
                }

                _ugolkiRulesList[i].SetSelected(false);
            }

            ugolkiRulesListItem.SetSelected(true);
            OnRuleSelected(ugolkiRulesListItem.Model);
        }

        private void OnLocalizationChanged()
        {
            for (int i = 0; i < _ugolkiRulesList.Count; i++)
            {
                _ugolkiRulesList[i].OnLocalizationChanged();
            }
        }

        private void OnRuleSelected(string rule)
        {
            RuleSelected?.Invoke(rule);
        }
    }
}