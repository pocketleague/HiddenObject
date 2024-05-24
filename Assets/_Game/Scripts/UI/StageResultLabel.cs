using Scripts.Stages;
using Scripts.Translation;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( TextMeshProUGUI ) )]
    public class StageResultLabel : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        [Inject]
        private IStagesService _stagesService;
    
        private TextMeshProUGUI _label;

        public string successTranslationCode;
        public string failureTranslationCode;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _stagesService.OnStageFinished += OnStageFinished;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageFinished -= OnStageFinished;
        }

        private void OnStageFinished( int index, bool success )
        {
            _label.SetText( _translationService.GetTranslatedString( success ? successTranslationCode : failureTranslationCode ) );
        }
    }
}