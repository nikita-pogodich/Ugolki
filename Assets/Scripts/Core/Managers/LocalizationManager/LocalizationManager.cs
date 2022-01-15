using System;
using System.Collections.Generic;

namespace Core.Managers.LocalizationManager
{
    public class LocalizationManager : ILocalizationManager
    {
        private LanguageInfo _currentLanguage;
        private List<LanguageInfo> _languageInfos;
        private Dictionary<string, string> _languageTerms;

        public event Action OnLocalizationChanged;
        public LanguageInfo CurrentLanguage => _currentLanguage;

        public LocalizationManager()
        {
            //TODO get this data from scriptable object
            _currentLanguage = new LanguageInfo {Code = "en", DisplayName = "English", Icon = "English"};
            _languageInfos = new List<LanguageInfo> {_currentLanguage};
            _languageTerms = new Dictionary<string, string>
            {
                {"main_menu_game_title", "Ugolki"},
                {"start_game_button", "Start Game"},
                {"ugolki_rule_1", "Pieces can jump over another diagonally"},
                {"ugolki_rule_2", "Pieces can jump vertically and horizontally"},
                {"ugolki_rule_3", "Pieces cannot jump, but only take one step in either direction"}
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

        public void SetLocale(string key)
        {
            OnLocalizationChanged?.Invoke();
        }

        public List<LanguageInfo> GetLocales()
        {
            return _languageInfos;
        }
    }
}