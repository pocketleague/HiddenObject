using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.StateManager
{
    public interface IState
    {
        void Begin();
        void End();
    }
}

public enum EState
{
    None,
    HomeCenter,
    GameplayCenter,
    RewardCenter,
    LevelSelection
}