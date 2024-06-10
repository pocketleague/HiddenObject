using System;
using Scripts.Core.StateManager;

namespace Scripts.Core.RewardCenter
{
    public class RewardCenterService : IRewardCenterService, IState
    {
        public event Action OnRewardCenterStarted = delegate { };
        public event Action OnRewardCenterEnded = delegate { };

        public void Begin()
        {
            OnRewardCenterStarted.Invoke();
        }

        public void End()
        {
            OnRewardCenterEnded.Invoke();
        }
    }
}
