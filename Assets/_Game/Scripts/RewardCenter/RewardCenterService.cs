using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Core;
using System;

namespace RewardCenter
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
