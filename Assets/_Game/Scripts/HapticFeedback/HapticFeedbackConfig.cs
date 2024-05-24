using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Scripts.HapticFeedback
{
    [CreateAssetMenu( menuName = ( "Configs/HapticFeedbackConfig" ), fileName = "HapticFeedbackConfig" )]
    public class HapticFeedbackConfig : ScriptableObject
    {
        public float minimumHapticInterval = 0.1f;

        public HapticTypes stageFinishedSuccess;
        public HapticTypes stageFinishedFailure;

        public HapticTypes uiButtonPressed;
    }
}