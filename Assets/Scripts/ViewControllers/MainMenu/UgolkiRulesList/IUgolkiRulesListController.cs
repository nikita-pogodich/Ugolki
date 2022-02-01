using System;
using Core.MVC;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public interface IUgolkiRulesListController : IController<IUgolkiRulesListView, IUgolkiRulesListModel>
    {
        event Action<string> RuleSelected;
        void SelectDefaultRule();
    }
}