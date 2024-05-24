using System;

namespace Scripts.Audio
{
    public interface ISfxService
    {
        event Action OnSfxEnabledChanged;
        
        public bool IsSfxEnabled { get; }
            
        void ToggleSfxEnabled();
        
        void PlayUiTapPositive();
        void PlayUiTapNegative();
        void PlayUiTapPurchase();
    }
}
