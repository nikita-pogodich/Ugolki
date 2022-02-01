using System;
using Core.MVC;
using ViewControllers.MainMenu.UgolkiRulesList;

namespace ViewControllers.MainMenu
{
    public interface IMainMenuView : IView
    {
        event Action StartGame;
        event Action ExitGame;
        IUgolkiRulesListView UgolkiRulesList { get; }
        void SetShown(bool isShown);
    }
}