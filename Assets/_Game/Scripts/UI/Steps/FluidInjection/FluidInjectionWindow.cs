using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;
using Scripts.FluidInjection;
using Zenject;
using Scripts.GameplayStates;

namespace Scripts.UI
{
    [RequireComponent(typeof(Window))]
    public class FluidInjectionWindow : MonoBehaviour
    {
        private FluidInjectionConfig        _config;
        private IFluidInjectionService      _service;
        private IGameplayStatesService      _gameplayStatesService;

        private IStagesService _stagesService;

        private Window _targetWindow;

        [SerializeField]
        private Button _finishStepButton;

        [Inject]
        private void Construct(FluidInjectionConfig config, IFluidInjectionService service, IGameplayStatesService gameplayStatesService, IStagesService stagesService)
        {
            _config =                       config;
            _service =                      service;
            _gameplayStatesService =        gameplayStatesService;
            _stagesService =                stagesService;

            Init();
        }

        private void Init()
        {
            _targetWindow = GetComponent<Window>();

            _stagesService.OnStageFinished += OnStageFinished;
            _stagesService.OnStageStarted += OnGameStarted;

            _finishStepButton.onClick.AddListener(OnFinish);
        }

        private void OnGameStarted(int stageId)
        {
            Debug.Log("OnStageStarted");

            _targetWindow.Open();
        }

        private void OnStageFinished(int stageId, bool success)
        {
            Debug.Log("OnStageFinished");
            _targetWindow.Close();
        }


        private void OnFinish()
        {
            Debug.Log("on finish");

            _gameplayStatesService.WinCurrentState();
        }

        private void OnDestroy()
        {
            _stagesService.OnStageFinished -= OnStageFinished;
            _stagesService.OnStageStarted -= OnGameStarted;
        }

    }
}
