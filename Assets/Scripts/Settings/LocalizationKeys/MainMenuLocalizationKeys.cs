using System.Collections.Generic;
using UgolkiController.UgolkiRules;

namespace Settings.LocalizationKeys
{
    public class MainMenuLocalizationKeys
    {
        public const string StartGameButton = "start_game_button";
        public const string UgolkiRule1 = "ugolki_rule_1";
        public const string UgolkiRule2 = "ugolki_rule_2";
        public const string UgolkiRule3 = "ugolki_rule_3";
        
        public static readonly IReadOnlyDictionary<string, string> UgolkiRulesMap = new Dictionary<string, string>
        {
            {UgolkiRulesList.Rule1, UgolkiRule1},
            {UgolkiRulesList.Rule2, UgolkiRule2},
            {UgolkiRulesList.Rule3, UgolkiRule3}
        };
    }
}