using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class MainMenuWindow : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;
        
        private Window _targetWindow;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _stagesService.OnStageStarted += OnStageStarted;
            _stagesService.OnStageSpawned += OnNextStageRequested;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageStarted -= OnStageStarted;
            _stagesService.OnStageSpawned -= OnNextStageRequested;
        }

        private void OnNextStageRequested( int stageId, StageConfig stageConfig )
        {
            _targetWindow.Open();
        }

        private void OnStageStarted( int stageId )
        {
            _targetWindow.Close();
        }
    }
}