using MoreMountains.NiceVibrations;

namespace Scripts.HapticFeedback
{
    public interface IHapticFeedbackService
    {
        void TriggerHaptics( HapticTypes type, bool force = false );
    }
}
