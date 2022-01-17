using System;
using System.Collections.Generic;

namespace Core.Managers.LocalizationManager
{
    public interface ILocalizationManager
    {
        event Action LocalizationChanged;
        LanguageInfo CurrentLanguage { get; }

        string GetText(string key);
        string GetText(string key, string keyToReplace, string valueToReplace);

        void SetLocale(string key);
        List<LanguageInfo> GetLocales();
    }
}