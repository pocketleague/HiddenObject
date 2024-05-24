using System.Linq;
using Scripts.Stages;
using Zenject;

namespace Scripts.FeatureUnlock
{
    public class FeatureUnlockService : IFeatureUnlockService
    {
        private FeaturesUnlockConfig _config;
        private FeatureUnlockWindow  _window;
        private IStagesService       _stagesService;

        private int _previouslyCheckedStage = -1;

        [Inject]
        private void Construct( FeaturesUnlockConfig config, IStagesService stagesService )
        {
            _config        = config;
            _stagesService = stagesService;

            _stagesService.OnStageSpawned += CheckForUnlockedFeatures;
        }

        public void RegisterWindow( FeatureUnlockWindow window )
        {
            _window = window;
        }

        private void CheckForUnlockedFeatures( int stageId, StageConfig stageConfig )
        {
            if ( _previouslyCheckedStage == stageId )
                return;

            _previouslyCheckedStage = stageId;
            
            foreach ( var unlockableFeature in _config.unlockableFeatures.Where( unlockableFeature => unlockableFeature.showOnLevel == stageId ) )
            {
                _window?.ShowFeatureUnlock( unlockableFeature );
                return;
            }
        }
    }
}
