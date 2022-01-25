using System;
using Core.MVC;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public interface IUgolkiRulesListItemModel : IModel
    {
        string RuleKey { get; }
        event Action<string> RuleKeyChanged;
        void ChangeRuleKey(string title);
    }
}