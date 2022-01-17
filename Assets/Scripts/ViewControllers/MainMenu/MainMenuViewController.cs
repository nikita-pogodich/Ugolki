using System.Collections.Generic;
using Core.Managers.LocalizationManager;
using Core.Managers.PoolingManager;
using Core.Managers.ViewManager;
using Core.MVC;
using Settings;
using UgolkiController;
using UnityEngine;
using ViewControllers.MainMenu.UgolkiRulesList;

namespace ViewControllers.MainMenu
{
    public class MainMenuViewController : BaseViewController<MainMenuView, ViewModel>
    {
        private IViewManager _viewManager;
        private IUgolkiController _ugolkiController;
        private List<string> _ugolkiRules;

        private UgolkiRulesListViewController _ugolkiRulesList;

        public override ViewType ViewType => ViewType.Window;
        public override string Name => ViewNamesList.MainMenu;

        public MainMenuViewController(
            IViewManager viewManager,
            IUgolkiController ugolkiController,
            IPoolingManager poolingManager,
            ILocalizationManager localizationManager)
        {
            _viewManager = viewManager;
            _ugolkiController = ugolkiController;
            _ugolkiRulesList = new UgolkiRulesListViewController(poolingManager, localizationManager);
        }

        protected override void OnViewAdded()
        {
            this.View.StartGame += OnStartGame;
            this.View.ExitGame += OnExitGame;
            _ugolkiRulesList.RuleSelected += OnRuleSelected;

            _ugolkiRules = _ugolkiController.GetRules();
            _ugolkiRulesList.SetModel(_ugolkiRules);
            _ugolkiRulesList.SetView(this.View.UgolkiRulesList);
        }

        protected override void OnViewRemoved()
        {
            this.View.StartGame -= OnStartGame;
            this.View.ExitGame -= OnExitGame;
            _ugolkiRulesList.RuleSelected -= OnRuleSelected;
        }

        protected override void OnSetShown(bool isShown)
        {
            this.View.SetShown(isShown);
        }

        private void OnRuleSelected(string rule)
        {
            _ugolkiController.SetRule(rule);
        }

        private void OnStartGame()
        {
            _ugolkiController.StartGame();
            _viewManager.ShowView(ViewNamesList.UgolkiGame);
        }

        private void OnExitGame()
        {
            Application.Quit();
        }
    }
}