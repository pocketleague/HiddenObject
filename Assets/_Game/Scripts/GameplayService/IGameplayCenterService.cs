using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameplayCenter
{
    public interface IGameplayCenterService
    {
        event Action OnGamePlayStarted;
        event Action OnGamePlayEnded;
    }
}