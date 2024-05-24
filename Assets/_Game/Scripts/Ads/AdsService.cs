using System;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.Ads {
    public enum RewardedVideoType { None, MultiplyGold }

    public class AdsService : IAdsService
    {
        public event Action<string> OnInterstitialImpression = delegate{ };
        public event Action<string> OnInterstitialCompleted  = delegate{ };

        public event Action<RewardedVideoType> OnRewardedVideoImpression = delegate{ };
        public event Action<RewardedVideoType> OnRewardedVideoCompleted  = delegate{ };

        private IStagesService _stagesService;

        [Inject]
        private void Construct( IStagesService stagesService )
        {
            _stagesService = stagesService;
            _stagesService.OnStageFinished += ShowStageFinishedInterstitial;
            
#if TTP_CORE
            Tabtale.TTPlugins.TTPBanners.Show();
#endif
        }

        private void ShowStageFinishedInterstitial( int stageId, bool success )
        {
            try {
#if TTP_CORE
            Tabtale.TTPlugins.TTPInterstitials.Show( AdLocations.INTER_STAGE_FINISHED, OnInterFinished );
#endif
                OnInterstitialImpression.Invoke( AdLocations.INTER_STAGE_FINISHED );
            } catch ( Exception e ) {
                Debug.Log( "Could not show AD of location: " + AdLocations.INTER_SEGMENT_FINISHED );
            }
        }

        private void OnInterFinished()
        {
            OnInterstitialCompleted.Invoke( AdLocations.INTER_STAGE_FINISHED );
        }
        
        public bool IsRewardedVideoReady()
        {
#if TTP_CORE
            return Tabtale.TTPlugins.TTPRewardedAds.IsReady();
#endif

            return false;
        }
    }
}