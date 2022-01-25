using System;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public class UgolkiRulesListItemModel : IUgolkiRulesListItemModel
    {
        public string RuleKey { get; }
        public event Action<string> RuleKeyChanged;

        public UgolkiRulesListItemModel(string title)
        {
            RuleKey = title;
        }

        public void ChangeRuleKey(string title)
        {
            RuleKeyChanged?.Invoke(title);
        }
    }
}