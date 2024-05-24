using System;

namespace Scripts.Translation
{
    public interface ITranslationService
    {
        event Action<string> OnLanguageChanged;

        void ChangeLanguageToNext();
        void ChangeLanguageToPrevious();

        string GetTranslatedString(string code);
    }
}
