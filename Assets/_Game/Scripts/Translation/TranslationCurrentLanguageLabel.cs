using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Translation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TranslationCurrentLanguageLabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        
        private TextMeshProUGUI _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _translationService.OnLanguageChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            _translationService.OnLanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged( string languageCode )
        {
            UpdateLanguageLabel( _translationService.GetTranslatedString( "LANGUAGE_NAME" ) );
        }

        private void UpdateLanguageLabel( string languageName )
        {
            _label.SetText( languageName );
        }
    }
}
