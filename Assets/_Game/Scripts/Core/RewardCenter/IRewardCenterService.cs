using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Core.RewardCenter
{
    public interface IRewardCenterService
    {
        event Action OnRewardCenterStarted;
        event Action OnRewardCenterEnded;
    }
}
