using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using RewardCenter;
using Scripts.Core;
using Scripts.Core.LevelSelection;

namespace Scripts.UI
{
    public class RewardWindow : MonoBehaviour
    {
        [Inject] private IRewardCenterService _rewardCenterService;
        [Inject] private IStateManagerService _stateManager;
        [Inject] private ILevelSelectionService _levelSelectionService;
        private Window _targetWindow;

        [SerializeField] Button btnEnd;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _rewardCenterService.OnRewardCenterStarted += OnRewardCenterStarted;
            _rewardCenterService.OnRewardCenterEnded += OnRewardCenterEnded;

            btnEnd.onClick.AddListener(End);
        }

        private void OnDestroy()
        {
            _rewardCenterService.OnRewardCenterStarted -= OnRewardCenterStarted;
            _rewardCenterService.OnRewardCenterEnded -= OnRewardCenterEnded;
        }

        private void OnRewardCenterStarted()
        {
            _targetWindow.Open();
        }

        private void OnRewardCenterEnded()
        {
            _targetWindow.Close();
        }

        void End()
        {
            GamesData.Instance.SetProgression(GameAnalyticsSDK.GAProgressionStatus.Complete, _levelSelectionService.currentLevel.ToString());

            int i = _levelSelectionService.currentLevel;
            i++;
            _levelSelectionService.SelectLevel(i);
            _stateManager.ChangeState(EState.GameplayCenter);
        }
    }
}
