using MoreMountains.NiceVibrations;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.HapticFeedback
{
    public class HapticFeedbackService : IHapticFeedbackService
    {
        private HapticFeedbackConfig _config;
        private IStagesService _stagesService;

        private float _hapticTimer     = 0f;
        private bool  _currentlyActive = false;
        private bool  _hapticsPaused   = false;

        [Inject]
        private void Construct(HapticFeedbackConfig config, IStagesService stagesService)
        {
            _config        = config;
            _stagesService = stagesService;
            
            _currentlyActive = PlayerPrefs.GetInt( HapticsPrefsStrings.HAPTICS_ENABLED, MMVibrationManager.HapticsSupported() ? 1 : 0 ) == 1;

            ToggleHapticsButton.HapticsToggled += OnHapticsToggled;

            //UI
            HapticFeedbackButton.RequestHapticFeedback += OnButtonPressed;

            //Stages
            _stagesService.OnStageFinished += OnStageFinished;

#if TTP_CORE
            Tabtale.TTPlugins.TTPCore.PauseGameMusicEvent += OnPauseMusicRequested;
#endif
#if UNITY_IOS
            MMVibrationManager.iOSInitializeHaptics();
#endif
        }

        private void Update()
        {
            _hapticTimer -= Time.deltaTime;
        }

        private void OnHapticsToggled( bool value )
        {
            _currentlyActive = value;

            PlayerPrefs.SetInt( HapticsPrefsStrings.HAPTICS_ENABLED, _currentlyActive ? 1 : 0 );
        }

        private void OnPauseMusicRequested( bool shouldPause )
        {
            _hapticsPaused = shouldPause;
        }

        private void OnButtonPressed()
        {
            TriggerHaptics( _config.uiButtonPressed, true );
        }

        private void OnStageFinished( int stageId, bool success )
        {
            TriggerHaptics( success ? _config.stageFinishedSuccess : _config.stageFinishedFailure, true );
        }

        public void TriggerHaptics( HapticTypes type, bool force = false )
        {
            if ( _hapticsPaused )
                return;
            if ( !_currentlyActive )
                return;
            if ( _hapticTimer > 0 && !force )
                return;

            MMVibrationManager.Haptic( type );
            _hapticTimer = _config.minimumHapticInterval;
        }
    }
}
