using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using GameplayCenter;
using Scripts.Core;
using Scripts.Timer;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class GameplayWindow : MonoBehaviour
    {

        [Inject] private IGameplayCenterService _gameplayService;
        [Inject] private IStateManagerService   _stateManager;
        [Inject] private ITimerService          _timerService;


        private Window _targetWindow;

        [SerializeField] Button btnEnd, btnPenalty;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _gameplayService.OnGamePlayStarted  += OnGamePlayStarted;
            _gameplayService.OnGamePlayEnded    += OnGamePlayEnded;

            btnEnd.onClick.AddListener(End);
            btnPenalty.onClick.AddListener(Penalty);
        }

        private void OnDestroy()
        {
            _gameplayService.OnGamePlayStarted  -= OnGamePlayStarted;
            _gameplayService.OnGamePlayEnded    -= OnGamePlayEnded;
        }

        private void OnGamePlayStarted()
        {
            _targetWindow.Open();
        }

        private void OnGamePlayEnded()
        {
            _targetWindow.Close();
        }

        void End()
        {
            _stateManager.ChangeState(EState.RewardCenter);
        }

        void Penalty()
        {
            _timerService.Penalty();
        }
    }
}