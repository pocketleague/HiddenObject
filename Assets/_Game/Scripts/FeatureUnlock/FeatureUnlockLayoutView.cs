using System.Collections.Generic;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.FeatureUnlock
{
    public class FeatureUnlockLayoutView : MonoBehaviour
    {
        private FeaturesUnlockConfig   _config;
        private IStagesService        _stagesService;
        
        private RectTransform                 _transform;
        private List<FeatureUnlockWidgetView> _spawnedWidgets;

        [Inject]
        private void Construct( FeaturesUnlockConfig config, IStagesService stagesService )
        {
            _stagesService        = stagesService;
            _config               = config;
            _transform            = GetComponent<RectTransform>( );

            _spawnedWidgets = new List<FeatureUnlockWidgetView>( );

            stagesService.OnStageSpawned += RefreshProgress;
        }

        private void Start()
        {
            RefreshProgress( _stagesService.CurrentStageId, null );
        }

        private void RefreshProgress( int stageId, StageConfig stageConfig )
        {
            DespawnCurrentViews( );

            var ( previousFeature, nextFeature) = _config.GetPreviousAndNextFeatureForStage( stageId );

            if ( nextFeature == null )
                return;

            var unlockStageMin = previousFeature == null ? 0 : previousFeature.showOnLevel;
            var unlockStageMax = nextFeature.showOnLevel;
            var widgetsAmount  = unlockStageMax - unlockStageMin;

            for ( var i = 0; i < widgetsAmount; ++i )
            {
                var spawnedWidget = Instantiate( _config.widgetPrefab, _transform );
                spawnedWidget.Initialize( stageId > unlockStageMin + i, stageId == unlockStageMin + i, i == widgetsAmount - 1 );
                _spawnedWidgets.Add( spawnedWidget );
            }
            
            void DespawnCurrentViews()
            {
                foreach ( var progressStage in _spawnedWidgets )
                {
                    Destroy( progressStage.gameObject );
                }

                _spawnedWidgets.Clear( );
            }
        }
    }
}
