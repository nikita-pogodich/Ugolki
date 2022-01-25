using System;
using System.Collections.Generic;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public class UgolkiRulesListModel : IUgolkiRulesListModel
    {
        public List<string> Rules { get; }
        public event Action<List<string>> RulesChanged;

        public UgolkiRulesListModel(List<string> rules)
        {
            Rules = rules;
        }

        public void ChangeRule(List<string> rules)
        {
            RulesChanged?.Invoke(rules);
        }
    }
}