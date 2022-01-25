using System;
using System.Collections.Generic;
using Core.MVC;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public interface IUgolkiRulesListModel : IModel
    {
        List<string> Rules { get; }
        event Action<List<string>> RulesChanged;
        void ChangeRule(List<string> rules);
    }
}