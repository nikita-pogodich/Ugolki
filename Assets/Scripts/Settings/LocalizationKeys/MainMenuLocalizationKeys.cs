using System.Collections.Generic;
using UgolkiController;

namespace Settings.LocalizationKeys
{
    public class MainMenuLocalizationKeys
    {
        public const string MainMenuGameTitle = "main_menu_game_title";
        public const string StartGameButton = "start_game_button";
        public const string UgolkiRule1 = "ugolki_rule_1";
        public const string UgolkiRule2 = "ugolki_rule_2";
        public const string UgolkiRule3 = "ugolki_rule_3";
        
        public static readonly IReadOnlyDictionary<string, string> UgolkiRulesMap = new Dictionary<string, string>
        {
            {UgolkiRules.Rule1, UgolkiRule1},
            {UgolkiRules.Rule2, UgolkiRule2},
            {UgolkiRules.Rule3, UgolkiRule3}
        };
    }
}