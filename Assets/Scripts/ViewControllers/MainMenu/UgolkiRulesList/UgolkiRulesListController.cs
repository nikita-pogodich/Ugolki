using System;
using System.Collections.Generic;
using Core.Managers.LocalizationManager;
using Core.MVC;
using Settings;
using ViewControllers.MainMenu.UgolkiRulesListItem;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public class UgolkiRulesListController :
        BaseController<IUgolkiRulesListView, IUgolkiRulesListModel>,
        IUgolkiRulesListController
    {
        private IViewFactory _viewFactory;
        private ILocalizationManager _localizationManager;

        private List<IUgolkiRulesListItemController> _ugolkiRulesList =
            new List<IUgolkiRulesListItemController>();

        private const int _defaultSelectedRule = 0;

        public event Action<string> RuleSelected;

        public UgolkiRulesListController(
            IUgolkiRulesListView view,
            IUgolkiRulesListModel model,
            IViewFactory viewFactory,
            ILocalizationManager localizationManager) : base(view, model)
        {
            _viewFactory = viewFactory;
            _localizationManager = localizationManager;

            _localizationManager.LocalizationChanged += OnLocalizationChanged;
            model.RulesChanged += OnUgolkiRulesListChanged;

            OnUgolkiRulesListChanged(model.Rules);
        }

        public void SelectDefaultRule()
        {
            OnRuleSelected(_ugolkiRulesList[_defaultSelectedRule]);
        }

        protected override void OnDispose()
        {
            ClearRulesList();

            _localizationManager.LocalizationChanged -= OnLocalizationChanged;
            this.Model.RulesChanged -= OnUgolkiRulesListChanged;
        }

        private void ClearRulesList()
        {
            for (int i = 0; i < _ugolkiRulesList.Count; i++)
            {
                _viewFactory.Destroy(ResourceNamesList.UgolkiRulesListItem, _ugolkiRulesList[i].View);
                _ugolkiRulesList[i].Selected -= OnRuleSelected;
            }

            _ugolkiRulesList.Clear();
        }

        private void OnUgolkiRulesListChanged(List<string> rulesList)
        {
            for (int i = 0; i < rulesList.Count; i++)
            {
                IUgolkiRulesListItemView ugolkiRulesListItemView =
                    _viewFactory.Create<IUgolkiRulesListItemView>(ResourceNamesList.UgolkiRulesListItem);

                IUgolkiRulesListItemModel ugolkiRulesListItemModel = new UgolkiRulesListItemModel(rulesList[i]);

                UgolkiRulesListItemController rulesListItem = new UgolkiRulesListItemController(
                    ugolkiRulesListItemView,
                    ugolkiRulesListItemModel,
                    _localizationManager);

                rulesListItem.Selected += OnRuleSelected;
                _ugolkiRulesList.Add(rulesListItem);

                this.View.AddItem(ugolkiRulesListItemView);
            }
        }

        private void OnRuleSelected(IUgolkiRulesListItemController ugolkiRulesListItem)
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
            RuleSelected?.Invoke(ugolkiRulesListItem.Model.RuleKey);
        }

        private void OnLocalizationChanged()
        {
            for (int i = 0; i < _ugolkiRulesList.Count; i++)
            {
                _ugolkiRulesList[i].OnLocalizationChanged();
            }
        }
    }
}