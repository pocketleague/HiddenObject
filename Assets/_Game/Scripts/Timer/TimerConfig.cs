using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Timer
{
    [CreateAssetMenu(fileName = "TimerConfig", menuName = "Configs/TimerConfig")]
    public class TimerConfig : ScriptableObject
    {
        public int time_penalty;
        public int time_freeze;

    }
}