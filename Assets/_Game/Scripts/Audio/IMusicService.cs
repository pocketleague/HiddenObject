using System;

namespace Scripts.Audio
{
    public interface IMusicService
    {
        event Action OnMusicEnabledChanged;
        
        public bool IsMusicEnabled { get; }

        void ToggleMusicEnabled( );
    }
}
