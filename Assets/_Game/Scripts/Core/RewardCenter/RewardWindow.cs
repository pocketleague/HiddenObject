using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using RewardCenter;
using Scripts.Core;

namespace Scripts.UI
{
    public class RewardWindow : MonoBehaviour
    {
        [Inject] private IRewardCenterService _rewardCenterService;
        [Inject] private IStateManagerService _stateManager;

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
            _stateManager.ChangeState(EState.HomeCenter);
        }
    }
}
