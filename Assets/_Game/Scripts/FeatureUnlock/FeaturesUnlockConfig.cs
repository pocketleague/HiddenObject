using System.Collections.Generic;
using UnityEngine;

namespace Scripts.FeatureUnlock
{
    [CreateAssetMenu( fileName = "FeaturesUnlockConfig", menuName = "Configs/FeaturesUnlockConfig" )]
    public class FeaturesUnlockConfig : ScriptableObject
    {
        public List<FeatureUnlockConfig> unlockableFeatures;
        public List<FeatureUnlockConfig> progressBarFeatures;

        public FeatureUnlockWidgetView widgetPrefab;

        public ( FeatureUnlockConfig, FeatureUnlockConfig ) GetPreviousAndNextFeatureForStage( int currentStageId )
        {
            FeatureUnlockConfig currentFeature = null;
            FeatureUnlockConfig nextFeature    = null;

            foreach ( var feature in progressBarFeatures )
            {
                if ( feature.showOnLevel <= currentStageId )
                {
                    currentFeature = feature;
                }
                else
                {
                    nextFeature = feature;
                    break;
                }
            }

            return ( currentFeature, nextFeature );
        }
    }
}