using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class GameplayWindow : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;
        
        private Window _targetWindow;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _stagesService.OnStageFinished += OnStageFinished;
            _stagesService.OnStageStarted  += OnGameStarted;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageFinished -= OnStageFinished;
            _stagesService.OnStageStarted  -= OnGameStarted;
        }

        private void OnGameStarted( int stageId )
        {
            _targetWindow.Open();
        }

        private void OnStageFinished( int stageId, bool success )
        {
            _targetWindow.Close();
        }
    }
}