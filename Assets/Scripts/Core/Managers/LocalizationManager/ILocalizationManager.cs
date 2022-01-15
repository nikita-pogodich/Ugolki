using System;
using System.Collections.Generic;

namespace Core.Managers.LocalizationManager
{
    public interface ILocalizationManager
    {
        event Action OnLocalizationChanged;
        LanguageInfo CurrentLanguage { get; }

        string GetText(string key);

        void SetLocale(string key);
        List<LanguageInfo> GetLocales();
    }
}