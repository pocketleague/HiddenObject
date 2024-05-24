using System;
using Scripts.Stages;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace Scripts.Audio
{
    public class MusicService : IMusicService
    {
        private MusicConfig _config;
        private IStagesService _stagesService;

        public event Action OnMusicEnabledChanged = delegate { };

        private Dictionary<string, MusicClipConfig> _clipsById;
        private AudioSource _source;

        public bool IsMusicEnabled { get; private set; }

        private MusicClipConfig _currentClip;

        [Inject]
        private void Construct(MusicConfig musicConfig, IStagesService stagesService)
        {
            _config = musicConfig;
            _stagesService = stagesService;

            _clipsById = new Dictionary<string, MusicClipConfig>();
            _config.clips.ForEach(clip => _clipsById.Add(clip.id, clip));

            _source = new GameObject("AudioMusicSource").AddComponent<AudioSource>();
            _source.loop = true;
            IsMusicEnabled = PlayerPrefs.GetInt(AudioPrefsStrings.MUSIC_ENABLED, 1) == 1;

            RefreshAudioSource();

            //_stagesService.OnStageStarted += PlayGameplayMusic;
            _stagesService.OnStageSpawned += PlayMainMenuMusic;
            //idleService.OnIdleInitialised += PlayGameTheme;

#if TTP_CORE
            //Tabtale.TTPlugins.TTPCore.PauseGameMusicEvent += DisableAudio;
#endif
        }

        private void DisableAudio(bool shouldPauseMusic)
        {
            _source.mute = shouldPauseMusic || !IsMusicEnabled;
        }

        public void ToggleMusicEnabled()
        {
            IsMusicEnabled = !IsMusicEnabled;
            PlayerPrefs.SetInt(AudioPrefsStrings.MUSIC_ENABLED, IsMusicEnabled ? 1 : 0);
            RefreshAudioSource();

            OnMusicEnabledChanged.Invoke();
        }

        private void PlayMainMenuMusic(int stageId, StageConfig stageConfig)
        {
            PlayMusic("background1");
        }

        void PlayGameTheme()
        {
            PlayMusic("background2");
        }

        public void PlayMusic(string id)
        {
            if (_currentClip != null)
            {
                if (_currentClip.id == id)
                    return;
            }

            _currentClip = _clipsById[id];
            PlayMusic(_currentClip.clip);
        }

        void PlayMusic(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }

        private void RefreshAudioSource()
        {
            _source.volume = _config.volume;
            _source.mute = !IsMusicEnabled;
        }
    }
}
