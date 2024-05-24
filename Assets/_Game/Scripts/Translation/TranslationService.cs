using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scripts.Translation
{
    public class TranslationService : ITranslationService
    {
        public event Action<string> OnLanguageChanged = delegate{ };

        private TranslationConfig          _config;
        private string                     _currentLanguageId;
        private Dictionary<string, string> _currentLanguageData;

        [Inject]
        private void Construct(TranslationConfig config)
        {
            _config = config;
            
            LoadSelectedLanguage();
        }

        private void LoadSelectedLanguage()
        {
            if ( PlayerPrefs.HasKey( "LANG" ) ) {
                ChangeLanguage( PlayerPrefs.GetString( "LANG" ) );
                return;
            } 
            
            foreach(var supportedLanguage in _config.expectedLanguages.Where(supportedLanguage => supportedLanguage.language == Application.systemLanguage))
            {
                ChangeLanguage( supportedLanguage.code );
                return;
            }

            ChangeLanguage( "en-us" );
        }

        private void ChangeLanguage( string languageCode )
        {
            _currentLanguageId   = languageCode;
            _currentLanguageData = _config.GetLanguageData( _currentLanguageId );

            OnLanguageChanged.Invoke( _currentLanguageId );
            PlayerPrefs.SetString( "LANG", languageCode );
        }

        public void ChangeLanguageToNext()
        {
            for ( var i = 0; i < _config.GetSupportedLanguages().Count; ++i ) {
                if ( _config.GetSupportedLanguages()[i] != _currentLanguageId )
                    continue;

                ChangeLanguage( _config.GetSupportedLanguages()[( i + 1 ) % _config.GetSupportedLanguages().Count] );
                return;
            }
        }

        public void ChangeLanguageToPrevious()
        {
            for ( var i = 0; i < _config.GetSupportedLanguages().Count; ++i ) {
                if ( _config.GetSupportedLanguages()[i] != _currentLanguageId )
                    continue;

                if ( i > 0 )
                    ChangeLanguage( _config.GetSupportedLanguages()[i - 1] );
                else
                    ChangeLanguage( _config.GetSupportedLanguages()[_config.GetSupportedLanguages().Count - 1] );
                return;
            }
        }

        public string GetTranslatedString( string code ) => _currentLanguageData.ContainsKey( code ) ? _currentLanguageData[code] : code;
    }
}
