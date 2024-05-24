using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Translation
{
    [System.Serializable]
    public struct UnityCountryCode
    {
        public SystemLanguage language;
        public string         code;
    }

    [CreateAssetMenu( menuName = ( "Configs/TranslationConfig" ), fileName = "TranslationConfig" )]
    public class TranslationConfig : ScriptableObject
    {
        public TextAsset              translationCSV;
        public List<UnityCountryCode> expectedLanguages;

        private Dictionary<string, Dictionary<string,string>> _languageDatabase;
        private List<string>                                  _supportedLanguages;

        private void InitializeLanguageDatabase()
        {
            //Generate Dictionary
            _languageDatabase = new Dictionary<string, Dictionary<string, string>>();

            List<string> linesOfText = new List<string>( translationCSV.text.Split('\n') );
            for ( int i = 0; i < linesOfText.Count; ++i ) {
                if ( linesOfText[i][linesOfText[i].Length - 1] == '\r' )
                    linesOfText[i] = linesOfText[i].Remove( linesOfText[i].Length - 1 );
            }
            for ( int i = 1; i < linesOfText[0].Split(';').Length; ++i ) {
                Dictionary<string, string> languageData = new Dictionary<string, string>();
                for ( int j = 1; j < linesOfText.Count; ++j ) {
                    languageData.Add( linesOfText[j].Split( ';' )[0], linesOfText[j].Split( ';' )[i] );
                }
                _languageDatabase.Add( linesOfText[0].Split( ';' )[i], languageData );
            }

            //Detect supported languages
            _supportedLanguages = new List<string>();
            foreach ( var language in _languageDatabase ) {
                _supportedLanguages.Add( language.Key );
            }
        }

        public Dictionary<string,string> GetLanguageData( string languageKey )
        {
            if ( _languageDatabase == null )
                InitializeLanguageDatabase();

            if ( _languageDatabase.ContainsKey( languageKey ) )
                return _languageDatabase[languageKey];
            else
                return _languageDatabase["en-us"];
        }

        public List<string> GetSupportedLanguages()
        {
            if ( _languageDatabase == null )
                InitializeLanguageDatabase();

            return _supportedLanguages;
        }
    }
}