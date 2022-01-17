using System;
using System.Collections.Generic;

namespace Core.Managers.LocalizationManager
{
    public class StubLocalizationManager : ILocalizationManager
    {
        private LanguageInfo _currentLanguage;
        private List<LanguageInfo> _languageInfos;
        private Dictionary<string, string> _languageTerms;

        public event Action LocalizationChanged;
        public LanguageInfo CurrentLanguage => _currentLanguage;

        public StubLocalizationManager()
        {
            _currentLanguage = new LanguageInfo {Code = "en", DisplayName = "English", Icon = "English"};
            _languageInfos = new List<LanguageInfo> {_currentLanguage};
            _languageTerms = new Dictionary<string, string>
            {
                {"main_menu_game_title", "Ugolki"},
                {"start_game_button", "Start Game"},
                {"ugolki_rule_1", "Pieces can jump over another diagonally"},
                {"ugolki_rule_2", "Pieces can jump vertically and horizontally"},
                {"ugolki_rule_3", "Pieces cannot jump, but only take one step in either direction"},
                {"white_moves_count", "White: [moves]"},
                {"black_moves_count", "Black: [moves]"},
                {"current_player", "Current player:<br>[player]"},
                {"white_player", "White"},
                {"black_player", "Black"},
                {"not_your_move_message", "Not your move"},
                {"select_piece_message", "Select piece"},
                {"move_unreachable_message", "Move unreachable"},
            };
        }

        public string GetText(string key)
        {
            bool hasKey = _languageTerms.TryGetValue(key, out string result);
            if (hasKey == false)
            {
                result = key;
            }

            return result;
        }

        public string GetText(string key, string keyToReplace, string valueToReplace)
        {
            string sourceString = (this as ILocalizationManager).GetText(key);
            string result = ReplaceWithLocalizedString(sourceString, keyToReplace, valueToReplace);
            return result;
        }

        public void SetLocale(string key)
        {
            LocalizationChanged?.Invoke();
        }

        public List<LanguageInfo> GetLocales()
        {
            return _languageInfos;
        }

        private string ReplaceWithLocalizedString(
            string sourceString,
            string keyToReplace,
            string valToLocalizeAndReplace)
        {
            string result = sourceString;
            if (string.IsNullOrEmpty(sourceString) == false)
            {
                string replacementString = GetText(valToLocalizeAndReplace);
                result = result.Replace(keyToReplace, replacementString);
            }

            return result;
        }
    }
}