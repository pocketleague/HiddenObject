using System.Collections.Generic;
using Scripts.Ads;
using Scripts.Stages;
using Zenject;

namespace Scripts.Analytics
{
    public class AnalyticsService
    {
        private IStagesService _stagesService;
        private IAdsService _adsService;
    
        private int _currentStageId;

        private const string _missionStartedEventName  = "missionStarted";
        private const string _missionFinishedEventName = "missionCompleted";
        private const string _missionFailedEventName   = "missionFailed";
    
        private const string _rvLocationEventName   = "rvLocation";
        private const string _rvImpressionEventName = "rvImpression";
        private const string _rvCompletedEventName  = "rvWatched";

        [Inject]
        private void Construct( IStagesService stagesService, IAdsService adsService )
        {
            _stagesService = stagesService;
            _adsService    = adsService;
            
            //Stages
            _stagesService.OnStageSpawned  += OnStageSpawned;
            _stagesService.OnStageStarted  += OnStageStarted;
            _stagesService.OnStageFinished += OnStageFinished;
            _stagesService.OnStageSkipped  += ( i ) => OnStageFinished( i, true );
            
            //Ads
            RewardedVideoButton.RewardedLocation      += OnRewardedVideoLocation;
            RewardedVideoCanvasGroup.RewardedLocation += OnRewardedVideoLocation;
            _adsService.OnRewardedVideoImpression     += OnRewardedVideoImpression;
            _adsService.OnRewardedVideoCompleted      += OnRewardedVideoCompleted;
        }

        private void OnStageSpawned( int stageId, StageConfig config )
        {
            _currentStageId = stageId + 1;
        }

        private void OnStageStarted( int stageId )
        {
            var eventParams = new Dictionary<string, object>
                 {
                     { "userLevel"   , _currentStageId },
                     { "missionType" , "Levels" },
                     { "missionID"   , _currentStageId }
                 };

            FireCustomEvent( _missionStartedEventName, eventParams );
        }

        private void OnStageFinished( int stageId, bool success )
        {
            var eventParams = new Dictionary<string, object>
                 {
                     { "userLevel"   , _currentStageId },
                     { "missionType" , "Levels" },
                     { "missionID"   , _currentStageId }
                 };

            FireCustomEvent( success ? _missionFinishedEventName : _missionFailedEventName, eventParams );
        }

        private void OnRewardedVideoLocation( RewardedVideoType adType, bool isReady )
        {
            var eventParams = new Dictionary<string, object>
                 {
                     { "userLevel"   , _currentStageId },
                     { "missionType" , "Levels" },
                     { "missionID"   , _currentStageId },
                     { "rvType"      , adType.ToString() },
                     { "rvAvailable" , isReady }
                 };

            FireCustomEvent( _rvLocationEventName, eventParams );
        }

        private void OnRewardedVideoImpression( RewardedVideoType adType )
        {
            var eventParams = new Dictionary<string, object>
                 {
                     { "userLevel"   , _currentStageId },
                     { "missionType" , "Levels" },
                     { "missionID"   , _currentStageId },
                     { "rvType"      , adType.ToString() }
                 };

            FireCustomEvent( _rvImpressionEventName, eventParams );
        }

        private void OnRewardedVideoCompleted( RewardedVideoType adType )
        {
            var eventParams = new Dictionary<string, object>
                 {
                     { "userLevel"   , _currentStageId },
                     { "missionType" , "Levels" },
                     { "missionID"   , _currentStageId },
                     { "rvType"      , adType.ToString() }
                 };

            FireCustomEvent( _rvCompletedEventName, eventParams );
        }

        private void FireCustomEvent( string eventName, Dictionary<string, object> eventParams )
        {
#if TTP_CORE
        Tabtale.TTPlugins.TTPAnalytics.LogEvent( Tabtale.TTPlugins.AnalyticsTargets.ANALYTICS_TARGET_FIREBASE, eventName, eventParams, false );
#endif
        }
    }
}