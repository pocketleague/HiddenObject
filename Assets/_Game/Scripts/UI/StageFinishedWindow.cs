using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(Window))]
    public class StageFinishedWindow : MonoBehaviour
    {
        private IStagesService _stagesService;
        private Window         _targetWindow;
        
        [Inject]
        private void Construct( IStagesService stagesService )
        {
            _stagesService  = stagesService;
            
            _targetWindow = GetComponent<Window>();

            _stagesService.OnStageFinished += OnStageFinished;
            _stagesService.OnStageSpawned  += OnNextStageRequested;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageFinished -= OnStageFinished;
            _stagesService.OnStageSpawned  -= OnNextStageRequested;
        }

        private void OnNextStageRequested( int stageId, StageConfig stageConfig )
        {
            _targetWindow.Close();
        }

        private void OnStageFinished( int stageId, bool success )
        {
            _targetWindow.Open();
        }
    }
}