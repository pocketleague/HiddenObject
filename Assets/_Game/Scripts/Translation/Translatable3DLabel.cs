using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Translation
{
    [RequireComponent(typeof( TextMeshPro ) )]
    public class Translatable3DLabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        
        public string translationCode;
        public bool   updateOnStart = false;

        private TextMeshPro _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshPro>();

            _translationService.OnLanguageChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            _translationService.OnLanguageChanged -= OnLanguageChanged;
        }

        private void Start()
        {
            if ( !updateOnStart )
                return;

            _label.SetText( _translationService.GetTranslatedString( translationCode ) );
        }

        private void OnLanguageChanged( string languageCode )
        {
            _label.SetText( _translationService.GetTranslatedString( translationCode ) );
        }
    }
}
