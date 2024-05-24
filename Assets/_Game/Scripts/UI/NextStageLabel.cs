using Scripts.Stages;
using Scripts.Translation;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NextStageLabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        [Inject]
        private IStagesService _stagesService;
        
        public bool   addPrefix = false;
        public string prefixTranslationCode;

        private TextMeshProUGUI _label;
        private int             _currentStageIndex;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _stagesService.OnStageStarted         += OnStageStarted;
            _translationService.OnLanguageChanged += OnLanguageChanged;
        }

        void OnDestroy()
        {
            _stagesService.OnStageStarted         -= OnStageStarted;
            _translationService.OnLanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged( string language )
        {
            OnStageStarted( _currentStageIndex );
        }

        private void OnStageStarted( int stageIndex )
        {
            _currentStageIndex = stageIndex;

            if ( addPrefix )
                _label.SetText( ( prefixTranslationCode != string.Empty ? _translationService.GetTranslatedString( prefixTranslationCode ) + " " : "Level " ) + ( _currentStageIndex + 2 ).ToString() );
            else
                _label.SetText( ( _currentStageIndex + 2 ).ToString() );
        }
    }
}