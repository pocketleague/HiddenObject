using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Translation
{
    [RequireComponent(typeof( TextMeshProUGUI ) )]
    public class TranslatableUILabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        
        public string translationCode;

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
            _label.SetText( _translationService.GetTranslatedString( translationCode ) );
        }
    }
}
