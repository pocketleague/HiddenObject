using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Stages
{
    [RequireComponent( typeof(Button))]
    public class SkipStageButton : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;

        private Button      _button;
        private CanvasGroup _canvasGroup;

        private int _lastFailedStageId;

        private void Awake()
        {
            _button      = GetComponent<Button>( );
            _canvasGroup = GetComponent<CanvasGroup>( );
            
            _button.onClick.AddListener( OnButtonClicked );
            _stagesService.OnStageFinished += RefreshButtonState;
        }

        private void RefreshButtonState( int stageId, bool success )
        {
            var shouldShow = !success;

            if ( stageId != _lastFailedStageId )
            {
                _lastFailedStageId = stageId;
                shouldShow         = false;
            }
            
            _canvasGroup.alpha          = shouldShow ? 1f : 0f;
            _canvasGroup.interactable   = shouldShow;
            _canvasGroup.blocksRaycasts = shouldShow;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
            _stagesService.OnStageFinished -= RefreshButtonState;
        }

        private void OnButtonClicked()
        {
            _stagesService.SkipCurrentStage( );
        }
    }
}
