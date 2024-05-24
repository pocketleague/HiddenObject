using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class StageProgressBar : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;
    
        public float minWidth = 0f;
        public float maxWidth = 600f;

        private       RectTransform _targetImage;
        private       float         _targetFill = 0;
        private const float         _lerpSpeed  = 5f;

        private void Awake()
        {
            _targetImage = GetComponent<RectTransform>();

            _stagesService.OnStageProgressChanged += OnProgressChanged;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageProgressChanged -= OnProgressChanged;
        }

        private void Update()
        {
            _targetImage.sizeDelta = new Vector2( Mathf.Lerp( _targetImage.sizeDelta.x, Mathf.Lerp( minWidth, maxWidth, _targetFill), Time.deltaTime * _lerpSpeed ), _targetImage.sizeDelta.y );
        }

        private void OnProgressChanged( float progress )
        {
            _targetFill = progress;
            if ( _targetFill == 0 ) {
                _targetImage.sizeDelta = new Vector2( 0, _targetImage.sizeDelta.y );
            }
        }
    }
}