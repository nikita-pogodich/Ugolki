using System.Collections.Generic;
using Core.Managers.LocalizationManager;
using Core.Managers.ViewManager;
using Core.MVC;
using Settings;
using UgolkiController;
using UnityEngine;
using ViewControllers.MainMenu.UgolkiRulesList;

namespace ViewControllers.MainMenu
{
    public class MainMenuController : BaseController<IMainMenuView, IMainMenuModel>, IMainMenuController
    {
        private IViewManager _viewManager;
        private IUgolkiController _ugolkiController;
        private IUgolkiRulesListController _ugolkiRulesList;

        public override ViewType ViewType => ViewType.Window;
        public override string Name => ViewNamesList.MainMenu;

        public MainMenuController(
            IViewManager viewManager,
            IUgolkiController ugolkiController,
            IViewFactory viewFactory,
            ILocalizationManager localizationManager,
            IMainMenuView view,
            IMainMenuModel model = null) : base(view, model)
        {
            _viewManager = viewManager;
            _ugolkiController = ugolkiController;

            List<string> ugolkiRules = _ugolkiController.GetRules();
            IUgolkiRulesListModel ugolkiRulesListModel = new UgolkiRulesListModel(ugolkiRules);

            _ugolkiRulesList = new UgolkiRulesListController(
                view.UgolkiRulesList,
                ugolkiRulesListModel,
                viewFactory,
                localizationManager);

            view.StartGame += OnStartGame;
            view.ExitGame += OnExitGame;
            _ugolkiRulesList.RuleSelected += OnRuleSelected;

            _ugolkiRulesList.SelectDefaultRule();
        }

        protected override void OnDispose()
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