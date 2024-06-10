using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Timer
{
    public interface ITimerService
    {
        event Action<float> OnTimerChanged;
        event Action OnTimerEnded;
        
        void Penalty();
        void Freeze();
    }
}