using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using GameplayCenter;
using Scripts.Core;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class GameplayWindow : MonoBehaviour
    {

        [Inject] private IGameplayCenterService _gameplayService;
        [Inject] private IStateManagerService _stateManager;

        private Window _targetWindow;

        [SerializeField] Button btnEnd;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _gameplayService.OnGamePlayStarted  += OnGamePlayStarted;
            _gameplayService.OnGamePlayEnded    += OnGamePlayEnded;

            btnEnd.onClick.AddListener(End);
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
            _stateManager.StartNextState(EState.RewardCenter);
        }
    }
}