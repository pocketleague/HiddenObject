using System;
using System.Collections.Generic;
using Scripts.GameLoop;
using Scripts.Stages;
using UnityEngine;
using Zenject;
using Scripts.Gold;

namespace Scripts.Audio
{
    public class SfxService : ISfxService
    {
        public event Action OnSfxEnabledChanged = delegate { };

        private SfxConfig _config;
        private AudioSource _sourceOneShot;
        private AudioSource _sourceContinuous;

        private Dictionary<string, SfxClipConfig> _clipsById;

        public bool IsSfxEnabled { get; private set; }

        private SfxClipConfig _currentClip;
        private List<AudioClip> _pendingClips;
        private List<float> _pendingTimers;
        private List<float> _pendingPitches;

        [Inject]
        private void Construct(SfxConfig config, IStagesService stagesService, IPlayerLoop playerLoop, IGoldService goldService)
        {
            _config = config;
            IsSfxEnabled = PlayerPrefs.GetInt(AudioPrefsStrings.SFX_ENABLED, 1) == 1;
            _pendingClips = new List<AudioClip>();
            _pendingTimers = new List<float>();
            _pendingPitches = new List<float>();

            _clipsById = new Dictionary<string, SfxClipConfig>();
            _config.clips.ForEach(clip => _clipsById.Add(clip.id, clip));

            SpawnAudioSources();
            RefreshAudioSource();

            //stagesService.OnStageFinished += ( _, success ) => PlayLevelFinished( success );
            playerLoop.OnUpdateTick += HandlePlayPendingClips;
            goldService.OnGoldSpend += PlayGoldSpendSfx;

#if TTP_CORE
            Tabtale.TTPlugins.TTPCore.PauseGameMusicEvent += DisableAudio;
#endif
        }

        private void DisableAudio(bool shouldPauseMusic)
        {
            _sourceOneShot.mute = shouldPauseMusic || !IsSfxEnabled;
            _sourceContinuous.mute = shouldPauseMusic || !IsSfxEnabled;
        }

        private void HandlePlayPendingClips()
        {
            var hasPendingClips = false;

            for (var i = 0; i < _pendingTimers.Count; ++i)
            {
                if (_pendingTimers[i] <= 0f)
                    continue;

                _pendingTimers[i] -= Time.deltaTime;
                hasPendingClips = true;

                if (_pendingTimers[i] > 0f)
                    continue;

                PlaySfx(_pendingClips[i], 0f, _pendingPitches[i]);
            }

            if (hasPendingClips)
                return;

            _pendingTimers.Clear();
            _pendingClips.Clear();
        }

        private void SpawnAudioSources()
        {
            _sourceOneShot = new GameObject("AudioSfxSourceOneShot").AddComponent<AudioSource>();
            _sourceContinuous = new GameObject("AudioSfxSourceContinuous").AddComponent<AudioSource>();
            _sourceContinuous.loop = true;
        }

        private void RefreshAudioSource()
        {
            _sourceOneShot.mute = !IsSfxEnabled;
            _sourceContinuous.mute = !IsSfxEnabled;
        }

        public void ToggleSfxEnabled()
        {
            IsSfxEnabled = !IsSfxEnabled;
            PlayerPrefs.SetInt(AudioPrefsStrings.SFX_ENABLED, IsSfxEnabled ? 1 : 0);
            RefreshAudioSource();

            OnSfxEnabledChanged.Invoke();
        }

        public void PlaySfx(string id, float delay = 0f, float pitch = 1f) => PlaySfx(_clipsById[id].clip, delay, pitch);

        public void PlaySfx(AudioClip clip, float delay = 0f, float pitch = 1f)
        {
            if (delay <= 0f)
            {
                _sourceOneShot.PlayOneShot(clip);
                return;
            }

            _pendingClips.Add(clip);
            _pendingTimers.Add(delay);
            _pendingPitches.Add(pitch);
        }

        public void PlayContinuousSfx(string id)
        {
            if (_sourceContinuous.isPlaying)
            {
                _sourceContinuous.Stop();
            }

            _currentClip = _clipsById[id];
            _sourceContinuous.clip = _currentClip.clip;
            _sourceContinuous.volume = _currentClip.volume;
            _sourceContinuous.Play();
        }

        public void StopContinuousSfx(string id)
        {
            if (_currentClip == null)
                return;
            if (_currentClip.id != id)
                return;

            _sourceContinuous.Stop();
            _currentClip = null;
        }

        public void PlayLevelFinished(bool success, float delay) => PlaySfx(success ? "StageWin" : "StageFail", delay);

        public void PlayUiTapPositive() => PlaySfx("UITapPositive");

        public void PlayUiTapNegative() => PlaySfx("UITapNegative");

        public void PlayUiTapPurchase() => PlaySfx("UITapPurchase");

        public void PlayGoldSpendSfx() => PlaySfx("GoldSpend");

        public void PlayCustomerRequestMatched() => PlaySfx("CustomerRequestMatched");
    }
}
