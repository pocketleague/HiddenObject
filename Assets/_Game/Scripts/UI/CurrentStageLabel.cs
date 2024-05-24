using Scripts.Stages;
using Scripts.Translation;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( TextMeshProUGUI ) )]
    public class CurrentStageLabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        [Inject]
        private IStagesService _stagesService;
        
        public bool   addPrefix = true;
        public string prefixTranslationCode;

        private TextMeshProUGUI _label;
        private int             _currentStageIndex;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _stagesService.OnStageSpawned         += OnStageStarted;
            _translationService.OnLanguageChanged += OnLanguageChanged;

            OnStageStarted( _stagesService.CurrentStageId, _stagesService.CurrentStage );
        }

        void OnDestroy()
        {
            _stagesService.OnStageSpawned         -= OnStageStarted;
            _translationService.OnLanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged( string language )
        {
            OnStageStarted( _currentStageIndex, null );
        }

        private void OnStageStarted( int stageIndex, StageConfig config )
        {
            _currentStageIndex = stageIndex;

            if ( addPrefix )
                _label.SetText( ( prefixTranslationCode != string.Empty ? _translationService.GetTranslatedString( prefixTranslationCode ) + " " : "Level " ) + ( _currentStageIndex + 1 ).ToString() );
            else
                _label.SetText( ( _currentStageIndex + 1 ).ToString() );
        }
    }
}