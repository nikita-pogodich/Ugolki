using Core.MVC;
using ViewControllers.MainMenu.UgolkiRulesListItem;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public interface IUgolkiRulesListView : IView
    {
        void AddItem(IUgolkiRulesListItemView item);
    }
}